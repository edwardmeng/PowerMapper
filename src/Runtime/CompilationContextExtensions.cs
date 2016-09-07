using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal static class CompilationContextExtensions
    {
        private static readonly MethodInfo _referenceEqualsMethod;

        private static readonly MethodInfo _getTypeFromHandleMethod;

        static CompilationContextExtensions()
        {
#if NetCore
            _referenceEqualsMethod = typeof(object).GetTypeInfo().GetMethod("ReferenceEquals", BindingFlags.Public | BindingFlags.Static);
            _getTypeFromHandleMethod = typeof(Type).GetTypeInfo().GetMethod("GetTypeFromHandle");
#else
            _referenceEqualsMethod = typeof(object).GetMethod("ReferenceEquals", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object), typeof(object) }, null);
            _getTypeFromHandleMethod = typeof(Type).GetMethod("GetTypeFromHandle");
#endif
        }
        public static void EmitNullableExpression(this CompilationContext context, LocalBuilder local,
            Action<CompilationContext> nonNullExpression, Action<CompilationContext> nullExpression)
        {
            var label = context.DefineLabel();
            if (local.LocalType.IsNullable())
            {
#if NetCore
                var variableType = local.LocalType.GetTypeInfo();
#else
                var variableType = local.LocalType;
#endif
                context.Emit(OpCodes.Ldloca, local);
                context.EmitCall(variableType.GetProperty("HasValue").GetGetMethod());
                context.Emit(OpCodes.Brfalse, label);

                context.Emit(OpCodes.Ldloca, local);
                context.EmitCall(variableType.GetProperty("Value").GetGetMethod());
                context.CurrentType = variableType.GetGenericArguments()[0];
            }
            else
            {
                context.Emit(OpCodes.Ldloc, local);
                context.Emit(OpCodes.Ldnull);
                context.EmitCall(_referenceEqualsMethod);
                context.Emit(OpCodes.Brtrue, label);

                context.Emit(OpCodes.Ldloc, local);
                context.CurrentType = local.LocalType;
            }
            nonNullExpression(context);

            var labelEnd = context.DefineLabel();
            context.Emit(OpCodes.Br, labelEnd);

            context.MakeLabel(label);
            nullExpression(context);

            context.MakeLabel(labelEnd);
        }

        public static void EmitCast(this CompilationContext context, Type targetType)
        {
            if (context.CurrentType == targetType) return;
#if NetCore
            var currentTypeIsValue = context.CurrentType.GetTypeInfo().IsValueType;
            var targetTypeIsValue = targetType.GetTypeInfo().IsValueType;
#else
            var currentTypeIsValue = context.CurrentType.IsValueType;
            var targetTypeIsValue = targetType.IsValueType;
#endif
            if (!currentTypeIsValue && targetTypeIsValue)
            {
                context.Emit(OpCodes.Unbox_Any, targetType);
            }
            else if (currentTypeIsValue && !targetTypeIsValue)
            {
                context.Emit(OpCodes.Box, context.CurrentType);
                if (targetType != typeof(object) && targetType != typeof(Enum) && targetType != typeof(void))
                {
                    context.Emit(OpCodes.Castclass, targetType);
                }
            }
            else
            {
                if (context.CurrentType == typeof(long) || context.CurrentType == typeof(ulong) ||
                    context.CurrentType == typeof(int) || context.CurrentType == typeof(uint) ||
                    context.CurrentType == typeof(short) || context.CurrentType == typeof(ushort) ||
                    context.CurrentType == typeof(byte) || context.CurrentType == typeof(sbyte) ||
                    context.CurrentType == typeof(float) || context.CurrentType == typeof(double) || 
                    context.CurrentType == typeof(char) || context.CurrentType == typeof(bool))
                {
                    if (targetType == typeof(sbyte))
                    {
                        context.Emit(OpCodes.Conv_I1);
                    }
                    if (targetType == typeof(byte))
                    {
                        context.Emit(OpCodes.Conv_U1);
                    }
                    if (targetType == typeof(short) || targetType == typeof(char))
                    {
                        context.Emit(OpCodes.Conv_I2);
                    }
                    if (targetType == typeof(ushort))
                    {
                        context.Emit(OpCodes.Conv_U2);
                    }
                    if (targetType == typeof(int))
                    {
                        context.Emit(OpCodes.Conv_I4);
                    }
                    if (targetType == typeof(uint))
                    {
                        context.Emit(OpCodes.Conv_U4);
                    }
                    if (targetType == typeof(long))
                    {
                        context.Emit(OpCodes.Conv_I8);
                    }
                    if (targetType == typeof(ulong))
                    {
                        context.Emit(OpCodes.Conv_U8);
                    }
                    if (targetType == typeof(float))
                    {
                        context.Emit(OpCodes.Conv_R4);
                    }
                    if (targetType == typeof(double))
                    {
                        context.Emit(OpCodes.Conv_R8);
                    }
                }
                else if (currentTypeIsValue)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.Emit_InvalidCastType, context.CurrentType, targetType));
                }
                else if (targetType != typeof(object))
                {
                    context.Emit(OpCodes.Castclass, targetType);
                }
            }
            context.CurrentType = targetType;
        }

        public static void EmitDefault(this CompilationContext context, Type targetType)
        {
            var originalType = targetType;
            if (targetType.IsNullable())
            {
                var local = context.DeclareLocal(targetType);
                context.Emit(OpCodes.Ldloca, local);
                context.Emit(OpCodes.Initobj, targetType);
                context.Emit(OpCodes.Ldloc, local);
                context.CurrentType = originalType;
                return;
            }
#if NetCore
            if (!targetType.GetTypeInfo().IsValueType)
#else
            if (!targetType.IsValueType)
#endif
            {
                context.Emit(OpCodes.Ldnull);
                context.CurrentType = originalType;
                return;
            }
#if NetCore
            if (targetType.GetTypeInfo().IsEnum)
#else
            if (targetType.IsEnum)
#endif
            {
                targetType = Enum.GetUnderlyingType(targetType);
            }
            if (targetType == typeof(int) || targetType == typeof(uint) ||
                targetType == typeof(short) || targetType == typeof(ushort) ||
                targetType == typeof(byte) || targetType == typeof(sbyte) ||
                targetType == typeof(char) || targetType == typeof(bool))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.CurrentType = originalType;
                return;
            }
            if (targetType == typeof(long) || targetType == typeof(ulong))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.Emit(OpCodes.Conv_I8);
                context.CurrentType = originalType;
                return;
            }
            if (targetType == typeof(float))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.Emit(OpCodes.Conv_R4);
                context.CurrentType = originalType;
                return;
            }
            if (targetType == typeof(double))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.Emit(OpCodes.Conv_R8);
                context.CurrentType = originalType;
                return;
            }
            Func<MethodInfo, bool> coverterPredicate = method =>
            {
                if (method.Name == "op_Implicit")
                {
                    var parameters = method.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == typeof(int);
                }
                return false;
            };
#if NetCore
            var reflectingType = targetType.GetTypeInfo();
            if (targetType == typeof(decimal))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.EmitCall(reflectingType.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(coverterPredicate));
                context.CurrentType = originalType;
                return;
            }
#else
            var reflectingType = targetType;
            if (targetType == typeof(decimal))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.EmitCall(reflectingType.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault(coverterPredicate));
                context.CurrentType = originalType;
                return;
            }
#endif
            var field = reflectingType.GetField("Empty", BindingFlags.Public | BindingFlags.Static) ??
                        reflectingType.GetField("Zero", BindingFlags.Public | BindingFlags.Static) ??
                        reflectingType.GetField("MinValue", BindingFlags.Public | BindingFlags.Static);
            if (field != null)
            {
                context.Emit(OpCodes.Ldsfld, field);
                context.CurrentType = field.FieldType;
                context.EmitCast(originalType);
                return;
            }
            var property = reflectingType.GetProperty("Empty", BindingFlags.Public | BindingFlags.Static) ??
                           reflectingType.GetProperty("Zero", BindingFlags.Public | BindingFlags.Static) ??
                           reflectingType.GetProperty("MinValue", BindingFlags.Public | BindingFlags.Static);
            var getMethod = property?.GetGetMethod();
            if (getMethod != null)
            {
                context.EmitCall(getMethod);
                context.CurrentType = getMethod.ReturnType;
                context.EmitCast(originalType);
                return;
            }
            var targetLocal = context.DeclareLocal(targetType);
            context.Emit(OpCodes.Ldloca, targetLocal);
            context.Emit(OpCodes.Initobj, targetType);
            context.Emit(OpCodes.Ldloc, targetLocal);
            context.CurrentType = originalType;
        }

        public static void EmitCall(this CompilationContext context, MethodInfo method)
        {
            context.Emit(method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, method);
            context.CurrentType = method.ReturnType;
        }

        public static void EmitTypeOf(this CompilationContext context, Type targetType)
        {
            context.Emit(OpCodes.Ldtoken, targetType);
            context.Emit(OpCodes.Call, _getTypeFromHandleMethod);
        }
    }
}
