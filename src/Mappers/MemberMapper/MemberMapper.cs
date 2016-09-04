using System;
#if Net35
using System.Collections.Generic;
#else
using System.Collections.Concurrent;
#endif
using System.Reflection.Emit;

namespace PowerMapper
{
    internal abstract class MemberMapper
    {
        private readonly MappingContainer _container;
        private readonly MemberMapOptions _options;
        private ValueConverter _converter;
        private ValueMapper _mapper;

#if Net35
        private static readonly Dictionary<Triplet<MappingContainer, Type, Type>, Type> _genericMapperTypes
            = new Dictionary<Triplet<MappingContainer, Type, Type>, Type>();

        private void EnsureMapperType(Type sourceType, Type targetType, ModuleBuilder builder)
        {
            var key = Triplet.Create(_container, sourceType, targetType);
            if (!_genericMapperTypes.ContainsKey(key))
            {
                lock (_genericMapperTypes)
                {
                    if (!_genericMapperTypes.ContainsKey(key))
                    {
                        _genericMapperTypes.Add(key, CreateMapper(builder, sourceType, targetType));
                    }
                }
            }
        }

        private bool HasOption(MemberMapOptions option)
        {
            return (_options & option) == option;
        }
#else
        private static readonly ConcurrentDictionary<Triplet<MappingContainer, Type, Type>, Type> _genericMapperTypes
            = new ConcurrentDictionary<Triplet<MappingContainer, Type, Type>, Type>();

        private void EnsureMapperType(Type sourceType, Type targetType, ModuleBuilder builder)
        {
            _genericMapperTypes.GetOrAdd(Triplet.Create(_container, sourceType, targetType), key => CreateMapper(builder, key.Second, key.Third));
        }

        private bool HasOption(MemberMapOptions option)
        {
            return _options.HasFlag(option);
        }
#endif

        protected MemberMapper(MappingContainer container, MemberMapOptions options, MappingMember targetMember, ValueConverter converter)
        {
            _container = container;
            _options = options;
            TargetMember = targetMember;
            _converter = converter;
        }

        public MappingMember TargetMember { get; }

        public abstract Type SourceType { get; }


        protected virtual ValueConverter CreateConverter(Type sourceType, Type targetType)
        {
            if (HasOption(MemberMapOptions.Hierarchy))
            {
                ValueConverter converter;
                if (EnumerableValueConverter.TryCreate(sourceType, targetType, _container, out converter))
                {
                    return converter;
                }
            }
            return null;
        }

        protected virtual ValueMapper CreateMapper(Type sourceType, Type targetType)
        {
            if (HasOption(MemberMapOptions.Hierarchy))
            {
                ValueMapper mapper;
                if (EnumerableMapper.TryCreate(sourceType,targetType,_container,out mapper))
                {
                    return mapper;
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
            if ((_converter == null || _mapper == null) && HasOption(MemberMapOptions.Hierarchy) &&
                !(TargetMember.MemberType.IsValueType && targetType == sourceType))
            {
                EnsureMapperType(sourceType, targetType, builder);
            }
        }

        private Type CreateMapper(ModuleBuilder builder, Type sourceType, Type targetType)
        {
            var instanceMapperType = typeof(InstanceMapper<,>).MakeGenericType(sourceType, targetType);
            var instanceMapper = instanceMapperType.GetMethod("GetInstance").Invoke(null, new object[] { _container });
            var convertMethod = instanceMapperType.GetProperty("Converter").GetValue(instanceMapper, null);
            var mapperMethod = instanceMapperType.GetProperty("Mapper").GetValue(instanceMapper, null);

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
            var convertMethod = ReflectionHelper.GetConvertMethod(sourceType, targetType);
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
            var targetCanWrite = TargetMember.CanWrite(HasOption(MemberMapOptions.NonPublic));
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
            if (!HasOption(MemberMapOptions.Hierarchy) || !targetType.IsClass || targetType.IsNullable())
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
                var mapperType = _genericMapperTypes[Triplet.Create(_container, sourceType, targetType)];
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
