using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using PowerMapper.Properties;

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
                if (currentTypeIsValue)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.Emit_InvalidCastType, context.CurrentType, targetType));
                }
                if (targetType != typeof(object))
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
            if (!targetType.GetTypeInfo().IsEnum)
#else
            if (!targetType.IsEnum)
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
#if NetCore
            var reflectingType = targetType.GetTypeInfo();
            if (targetType == typeof(decimal))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.EmitCall(reflectingType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public));
                context.CurrentType = originalType;
                return;
            }
#else
            var reflectingType = targetType;
            if (targetType == typeof(decimal))
            {
                context.Emit(OpCodes.Ldc_I4_0);
                context.EmitCall(reflectingType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(int) }, null));
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
            var method = property?.GetGetMethod();
            if (method != null)
            {
                context.EmitCall(method);
                context.CurrentType = method.ReturnType;
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
