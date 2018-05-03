using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class EnumerableValueConverter : ValueConverter
    {
        private readonly MappingContainer _container;
        private readonly Type _sourceElementType;
        private readonly Type _targetElementType;
        private IInvokerBuilder _invokerBuilder;

        public EnumerableValueConverter(MappingContainer container, Type sourceElementType, Type targetElementType)
        {
            _container = container;
            _sourceElementType = sourceElementType;
            _targetElementType = targetElementType;
        }
        public override int Match(ConverterMatchContext context)
        {
            var sourceDistance = ReflectionHelper.GetDistance(context.SourceType, typeof(IEnumerable<>));
            if (sourceDistance == -1) return -1;
            var targetDistance = ReflectionHelper.GetDistance(context.TargetType, typeof(IEnumerable<>));
            if (targetDistance == -1) return -1;
            return sourceDistance + targetDistance;
        }

        public override void Compile(ModuleBuilder builder)
        {
            _invokerBuilder =
                (IInvokerBuilder)
                    Activator.CreateInstance(typeof(EnumerableConverterBuilder<,>).MakeGenericType(_sourceElementType, _targetElementType), _container);
            _invokerBuilder.Compile(builder);
        }

        public override void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
#if NETSTANDARD
            var reflectingTargetType = targetType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
#endif
            context.EmitCast(typeof(IEnumerable<>).MakeGenericType(_sourceElementType));
            _invokerBuilder.Emit(context);
            if (reflectingTargetType.IsGenericType)
            {
                var genericTypeDefinition = targetType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>))
                {
                    context.EmitCast(targetType);
                    context.CurrentType = targetType;
                    return;
                }
            }
            else if (targetType.IsArray)
            {
                context.EmitCast(targetType);
                context.CurrentType = targetType;
                return;
            }

            var labelNull = context.DefineLabel();
            var labelComplete = context.DefineLabel();
            var local = context.DeclareLocal(context.CurrentType);
            context.Emit(OpCodes.Stloc, local);

            context.Emit(OpCodes.Ldloc, local);
            context.Emit(OpCodes.Brfalse, labelNull);
            context.Emit(OpCodes.Ldloc, local);

            if (targetType.IsEnumerable(out var targetElementType))
            {
                var constructor = reflectingTargetType.GetConstructor(new[] { typeof(IEnumerable<>).MakeGenericType(targetElementType) }) ??
                                  reflectingTargetType.GetConstructor(new[] { typeof(IList<>).MakeGenericType(targetElementType) }) ??
                                  reflectingTargetType.GetConstructor(new[] { typeof(ICollection<>).MakeGenericType(targetElementType) }) ??
                                  reflectingTargetType.GetConstructor(new[] { targetElementType.MakeArrayType() });
                if (constructor != null)
                {
                    context.EmitCast(constructor.GetParameters()[0].ParameterType);
                    context.Emit(OpCodes.Newobj, constructor);
                }
                else
                {
                    var defaultConstructor = reflectingTargetType.GetConstructor(Type.EmptyTypes);
                    var addMethod = reflectingTargetType.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(method =>
                    {
                        if (method.Name != "Add") return false;
                        var parameters = method.GetParameters();
                        return parameters.Length == 1 && parameters[0].ParameterType == targetElementType;
                    }).FirstOrDefault();
                    if (defaultConstructor != null && addMethod != null)
                    {
                        var targetArrayType = targetElementType.MakeArrayType();
                        var targetArray = context.DeclareLocal(targetArrayType);
                        var targetInstance = context.DeclareLocal(targetType);
                        var index = context.DeclareLocal(typeof(int));

                        context.EmitCast(targetArrayType);
                        context.Emit(OpCodes.Stloc, targetArray);

                        context.Emit(OpCodes.Newobj, defaultConstructor);
                        context.Emit(OpCodes.Stloc, targetInstance);

                        // var i = 0;
                        context.Emit(OpCodes.Ldc_I4_0);
                        context.Emit(OpCodes.Stloc, index);

                        var labelEnd = context.DefineLabel();
                        context.Emit(OpCodes.Br_S, labelEnd);
                        var labelStart = context.DefineLabel();
                        context.MakeLabel(labelStart);

                        // target.Add(array[i]);
                        context.Emit(OpCodes.Ldloc, targetInstance);
                        context.Emit(OpCodes.Ldloc, targetArray);
                        context.Emit(OpCodes.Ldloc, index);
#if NETSTANDARD
                        var targetElementTypeInfo = targetElementType.GetTypeInfo();
                        if (targetElementTypeInfo.IsValueType && !targetElementTypeInfo.IsPrimitive)
#else
                        if (targetElementType.IsValueType && !targetElementType.IsPrimitive)
#endif
                        {
                            context.Emit(OpCodes.Ldelema, targetElementType);
                        }
                        else
                        {
                            context.Emit(OpCodes.Ldelem, targetElementType);
                        }
                        context.EmitCall(addMethod);

                        // i++
                        context.Emit(OpCodes.Ldloc, index);
                        context.Emit(OpCodes.Ldc_I4_1);
                        context.Emit(OpCodes.Add);
                        context.Emit(OpCodes.Stloc, index);


                        context.MakeLabel(labelEnd);
                        context.Emit(OpCodes.Ldloc, index);
                        context.Emit(OpCodes.Ldloc, targetArray);
                        context.Emit(OpCodes.Ldlen);
                        context.Emit(OpCodes.Conv_I4);
                        context.Emit(OpCodes.Blt_S, labelStart);

                        context.Emit(OpCodes.Ldloc, targetInstance);
                    }
                }
            }

            context.Emit(OpCodes.Br_S, labelComplete);
            context.MakeLabel(labelNull);
            context.Emit(OpCodes.Ldnull);
            context.MakeLabel(labelComplete);
            context.CurrentType = targetType;
        }

        public static bool TryCreate(Type sourceType, Type targetType, MappingContainer container, out ValueConverter converter)
        {
            converter = null;
            Type sourceElementType, targetElementType;
            if (sourceType.IsEnumerable(out sourceElementType) && targetType.IsEnumerable(out targetElementType))
            {
                converter = new EnumerableValueConverter(container, sourceElementType, targetElementType);
                return true;
            }
            return false;
        }
    }
}
