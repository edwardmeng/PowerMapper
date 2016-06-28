using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal abstract class MemberMapper
    {
        private readonly ObjectMapper _container;
        private readonly MemberMapOptions _options;
        private Converter _converter;
        private EnumerableMapper _mapper;

        private static readonly ConcurrentDictionary<Tuple<ObjectMapper, Type, Type>, Type> _genericMapperTypes
            = new ConcurrentDictionary<Tuple<ObjectMapper, Type, Type>, Type>();

        protected MemberMapper(ObjectMapper container, MemberMapOptions options, MappingMember targetMember, Converter converter)
        {
            _container = container;
            _options = options;
            TargetMember = targetMember;
            _converter = converter;
        }

        public MappingMember TargetMember { get; }

        public abstract Type SourceType { get; }

        protected virtual Converter CreateConverter(Type sourceType, Type targetType)
        {
            if (_options.HasFlag(MemberMapOptions.Hierarchy))
            {
                Type sourceEnumerableType, targetEnumerableType;
                if (Helper.ImplementsGeneric(sourceType, typeof(IEnumerable<>), out sourceEnumerableType) && Helper.ImplementsGeneric(targetType, typeof(IEnumerable<>), out targetEnumerableType))
                {
                    return new EnumerableConverter(_container, sourceEnumerableType.GetGenericArguments()[0], targetEnumerableType.GetGenericArguments()[0]);
                }
            }
            return null;
        }

        protected virtual EnumerableMapper CreateMapper(Type sourceType, Type targetType)
        {
            if (_options.HasFlag(MemberMapOptions.Hierarchy))
            {
                Type sourceEnumerableType, targetEnumerableType;
                if (Helper.ImplementsGeneric(sourceType, typeof(IEnumerable<>), out sourceEnumerableType) && Helper.ImplementsGeneric(targetType, typeof(IEnumerable<>), out targetEnumerableType))
                {
                    var sourceElementType = sourceEnumerableType.GetGenericArguments()[0];
                    var targetElementType = targetEnumerableType.GetGenericArguments()[0];
                    if (sourceElementType.IsValueType || sourceElementType.IsPrimitive)
                    {
                        return null;
                    }
                    if (targetElementType.IsValueType || targetElementType.IsPrimitive)
                    {
                        return null;
                    }
                    return new EnumerableMapper(_container, sourceElementType, targetElementType);
                }
            }
            return null;
        }

        public virtual void Compile(ModuleBuilder builder)
        {
            var sourceType = SourceType;
            var targetType = TargetMember.MemberType;
            if (_converter == null)
            {
                _converter = _container.Converters.Get(sourceType, targetType);
            }
            if (_converter == null)
            {
                _converter = CreateConverter(sourceType, targetType);
            }
            _converter?.Compile(builder);
            if (_mapper == null)
            {
                _mapper = CreateMapper(sourceType, targetType);
            }
            _mapper?.Compile(builder);
            if ((_converter == null || _mapper == null) && _options.HasFlag(MemberMapOptions.Hierarchy) &&
                !(TargetMember.MemberType.IsValueType && targetType == sourceType))
            {
                _genericMapperTypes.GetOrAdd(Tuple.Create(_container, sourceType, targetType), key => CreateMapper(builder, key.Item2, key.Item3));
            }
        }

        private Type CreateMapper(ModuleBuilder builder, Type sourceType, Type targetType)
        {
            var instanceMapperType = typeof(InstanceMapper<,>).MakeGenericType(sourceType, targetType);
            var instanceMapper = instanceMapperType.GetMethod("GetInstance").Invoke(null, new object[] { _container });
            var convertMethod = instanceMapperType.GetProperty("Converter").GetValue(instanceMapper);
            var mapperMethod = instanceMapperType.GetProperty("Mapper").GetValue(instanceMapper);

            var convertBuilder = (IInvokerBuilder)Activator.CreateInstance(typeof(FuncInvokerBuilder<,>).MakeGenericType(sourceType, targetType), convertMethod);
            convertBuilder.Compile(builder);
            var mapperBuilder = (IInvokerBuilder)Activator.CreateInstance(typeof(ActionInvokerBuilder<,>).MakeGenericType(sourceType, targetType), mapperMethod);
            mapperBuilder.Compile(builder);

            var typeBuilder = builder.DefineStaticType();
            // Declare Convert method.
            if (_converter == null)
            {
                var methodBuilder = typeBuilder.DefineStaticMethod("Convert");
                methodBuilder.SetReturnType(targetType);
                methodBuilder.SetParameters(sourceType);

                var il = methodBuilder.GetILGenerator();
                var context = new CompilationContext(il);
                il.Emit(OpCodes.Ldarg_0);
                context.CurrentType = sourceType;
                convertBuilder.Emit(context);
                il.Emit(OpCodes.Ret);
            }
            // Declare Map method.
            if (_mapper == null)
            {
                var methodBuilder = typeBuilder.DefineStaticMethod("Map");
                methodBuilder.SetParameters(sourceType, targetType);

                var il = methodBuilder.GetILGenerator();
                var context = new CompilationContext(il);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                mapperBuilder.Emit(context);
                il.Emit(OpCodes.Ret);
            }

            var type = typeBuilder.CreateType();
            return type;
        }

        private Action<CompilationContext> GetConvertEmitter(Type sourceType, Type targetType)
        {
            if (sourceType == targetType)
            {
                return context => { };
            }
            if (targetType.IsAssignableFrom(sourceType))
            {
                return context => context.EmitCast(targetType);
            }
            var convertMethod = Helper.GetConvertMethod(sourceType, targetType);
            if (convertMethod != null)
            {
                return context =>
                {
                    context.EmitCall(convertMethod);
                    context.CurrentType = targetType;
                };
            }
            return null;
        }

        public virtual void Emit(CompilationContext context)
        {
            var sourceType = SourceType;
            var targetType = TargetMember.MemberType;
            var targetCanWrite = TargetMember.CanWrite(_options.HasFlag(MemberMapOptions.NonPublic));
            if (targetCanWrite && _converter != null)
            {
                EmitSource(context);
                _converter.Emit(sourceType, targetType, context);
                EmitSetTarget(context);
                return;
            }
            if (!targetCanWrite && _mapper != null)
            {
                EmitSource(context);
                EmitSetTarget(context);
                _mapper.Emit(sourceType, targetType, context);
                return;
            }
            if (!_options.HasFlag(MemberMapOptions.Hierarchy) || !targetType.IsClass || Helper.IsNullable(targetType))
            {
                var converter = GetConvertEmitter(sourceType, targetType);
                if (converter != null)
                {
                    EmitSource(context);
                    converter(context);
                    EmitSetTarget(context);
                }
            }
            else
            {
                var key = Tuple.Create(_container, sourceType, targetType);
                var mapperType = _genericMapperTypes[key];
                if (targetCanWrite)
                {
                    EmitSource(context);
                    context.CurrentType = sourceType;
                    context.EmitCall(mapperType.GetMethod("Convert"));
                    EmitSetTarget(context);
                }
                else
                {
                    var sourceValue = context.DeclareLocal(sourceType);
                    EmitSource(context);
                    context.Emit(OpCodes.Stloc, sourceValue);

                    var targetValue = context.DeclareLocal(targetType);
                    ((IMemberBuilder)TargetMember).EmitGetter(context);
                    context.Emit(OpCodes.Stloc, targetValue);

                    context.Emit(OpCodes.Ldloc, sourceValue);
                    context.Emit(OpCodes.Ldloc, targetValue);
                    context.EmitCall(mapperType.GetMethod("Map"));
                }
            }
        }

        protected abstract void EmitSource(CompilationContext context);

        private void EmitSetTarget(CompilationContext context)
        {
            ((IMemberBuilder)TargetMember).EmitSetter(context);
        }
    }
}
