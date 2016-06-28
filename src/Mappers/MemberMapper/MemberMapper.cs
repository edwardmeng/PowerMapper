using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var typeBuilder = builder.DefineStaticType();
            // public static ObjectMapper Container;
            var field = typeBuilder.DefineField("Container", typeof(ObjectMapper),
                FieldAttributes.Public | FieldAttributes.Static);
            // Declare Convert method.
            if (_converter == null)
            {
                var methodBuilder = typeBuilder.DefineStaticMethod("Convert");
                methodBuilder.SetReturnType(targetType);
                methodBuilder.SetParameters(sourceType);

                var convertMethod = typeof(ObjectMapper).GetMethods().Where(method =>
                {
                    if (method.Name != "Map" || !method.IsGenericMethodDefinition) return false;
                    method = method.MakeGenericMethod(sourceType, targetType);
                    if (method.ReturnType != targetType) return false;
                    var parameters = method.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == sourceType;
                }).First();
                var il = methodBuilder.GetILGenerator();
                il.Emit(OpCodes.Ldsfld, field);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, convertMethod.MakeGenericMethod(sourceType, targetType));
                il.Emit(OpCodes.Ret);
            }
            // Declare Map method.
            if (_mapper == null)
            {
                var methodBuilder = typeBuilder.DefineStaticMethod("Map");
                methodBuilder.SetParameters(sourceType, targetType);

                var mapMethod = typeof(ObjectMapper).GetMethods().Where(method =>
                {
                    if (method.Name != "Map" || method.ReturnType != typeof(void) || !method.IsGenericMethodDefinition) return false;
                    method = method.MakeGenericMethod(sourceType, targetType);
                    var parameters = method.GetParameters();
                    return parameters.Length == 2 && parameters[0].ParameterType == sourceType && parameters[1].ParameterType == targetType;
                }).First();
                var il = methodBuilder.GetILGenerator();
                il.Emit(OpCodes.Ldsfld, field);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Callvirt, mapMethod.MakeGenericMethod(sourceType, targetType));
                il.Emit(OpCodes.Ret);
            }

            var type = typeBuilder.CreateType();
            Helper.SetStaticField(type, "Container", _container);
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
