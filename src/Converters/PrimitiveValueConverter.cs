using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class PrimitiveValueConverter : ValueConverter
    {
        private static readonly Type[] _primitiveTypes =
        {
            typeof (byte), typeof (sbyte), typeof (char), typeof (double), typeof (float), typeof (decimal), typeof (int),
            typeof (uint), typeof (short), typeof (ushort), typeof (long), typeof (ulong)
        };
        public override int Match(ConverterMatchContext context)
        {
            if (Equals(context.GetProperty(typeof(PrimitiveValueConverter)), true)) return -1;
            return ExecuteMatch(context);
        }

        private int ExecuteMatch(ConverterMatchContext context)
        {
            var targetType = context.TargetType;
            var sourceType = context.SourceType;
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
            var reflectingSourceType = sourceType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
            var reflectingSourceType = sourceType;
#endif
            if (targetType.IsNullable() && sourceType.IsNullable())
            {
                var distance = MatchConvert(reflectingSourceType.GetGenericArguments()[0], reflectingTargetType.GetGenericArguments()[0]);
                if (distance != -1)
                {
                    return distance + 2;
                }
            }
            if (targetType.IsNullable())
            {
                var distance = MatchConvert(sourceType, reflectingTargetType.GetGenericArguments()[0]);
                if (distance != -1)
                {
                    return distance + 1;
                }
            }
            if (sourceType.IsNullable())
            {
                var distance = MatchConvert(reflectingSourceType.GetGenericArguments()[0], targetType);
                if (distance != -1)
                {
                    return distance + 1;
                }
            }
            return MatchConvert(sourceType, targetType);
        }

        private int MatchConvert(Type sourceType, Type targetType)
        {
            var converter = FindConverter(sourceType, targetType);
            if (converter != null) return 1;
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
            var reflectingSourceType = sourceType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
            var reflectingSourceType = sourceType;
#endif
            if (reflectingTargetType.IsAssignableFrom(sourceType)) return 0;
            if (ReflectionHelper.GetConvertMethod(sourceType, targetType) != null) return 0;
            if (_primitiveTypes.Contains(sourceType) && _primitiveTypes.Contains(targetType)) return 0;
            var hasEnumeration = false;
            if (reflectingSourceType.IsEnum)
            {
                sourceType = Enum.GetUnderlyingType(sourceType);
                hasEnumeration = true;
            }
            if (reflectingTargetType.IsEnum)
            {
                targetType = Enum.GetUnderlyingType(targetType);
                hasEnumeration = true;
            }
            if (hasEnumeration)
            {
                return MatchConvert(sourceType, targetType) + 1;
            }
            return -1;
        }

        public override void Compile(ModuleBuilder builder)
        {
        }

        public override void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
            if (ExecuteEmit(sourceType, targetType, context))
            {
                context.CurrentType = targetType;
            }
        }

        private bool ExecuteEmit(Type sourceType, Type targetType, CompilationContext context)
        {
            if (targetType.IsNullable() && sourceType.IsNullable() && EmitBothNullable(context, sourceType, targetType)) return true;
            if (targetType.IsNullable() && EmitNullableTarget(context, sourceType, targetType)) return true;
            if (sourceType.IsNullable() && EmitNullableSource(context, sourceType, targetType)) return true;
            return EmitPrimitive(context, sourceType, targetType);
        }

        // When the target type is value type and the source type is the same type 
        // or can be explictly or implicitly converted to the target type.
        private bool EmitPrimitive(CompilationContext context, Type sourceType, Type targetType)
        {
            var converter = GetConvertEmitter(sourceType, targetType);
            if (converter != null)
            {
                converter(context);
                return true;
            }
            return false;
        }

        private bool EmitNullableSource(CompilationContext context, Type sourceType, Type targetType)
        {
#if NetCore
            var reflectingSourceType = sourceType.GetTypeInfo();
#else
            var reflectingSourceType = sourceType;
#endif
            var sourceUnderlingType = reflectingSourceType.GetGenericArguments()[0];
            var converter = GetConvertEmitter(sourceUnderlingType, targetType);
            if (converter != null)
            {
                var target = context.DeclareLocal(targetType);
                var local = context.DeclareLocal(sourceType);
                context.Emit(OpCodes.Stloc, local);

                context.EmitNullableExpression(local, ctx =>
                {
                    converter(ctx);
                    ctx.Emit(OpCodes.Stloc, target);
                }, ctx =>
                {
                    context.EmitDefault(targetType);
                    context.Emit(OpCodes.Stloc, target);
                });
                context.Emit(OpCodes.Ldloc, target);
                return true;
            }
            return false;
        }

        // When the target type is nullable value type and the source type is same as the underling type of the source type 
        // or can be explictly or implicitly converted to the underling type of the target type.
        private bool EmitNullableTarget(CompilationContext context, Type sourceType, Type targetType)
        {
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
#endif
            var converter = GetConvertEmitter(sourceType, reflectingTargetType.GetGenericArguments()[0]);
            if (converter != null)
            {
                converter(context);
                context.Emit(OpCodes.Newobj, reflectingTargetType.GetConstructors()[0]);
                return true;
            }
            return false;
        }

        private bool EmitBothNullable(CompilationContext context, Type sourceType, Type targetType)
        {
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
            var reflectingSourceType = sourceType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
            var reflectingSourceType = sourceType;
#endif
            var sourceUnderlingType = reflectingSourceType.GetGenericArguments()[0];
            var targetUnderlingyType = reflectingTargetType.GetGenericArguments()[0];
            // When the source and target member are the same nullbale type
            if (targetUnderlingyType == sourceUnderlingType)
            {
                return true;
            }
            // When the source and target member are not the same nullable type,
            // But their underlying type can be implicitly or explicitly converted.
            var converter = GetConvertEmitter(sourceUnderlingType, targetUnderlingyType);
            if (converter != null)
            {
                var target = context.DeclareLocal(targetType);
                var local = context.DeclareLocal(sourceType);
                context.Emit(OpCodes.Stloc, local);

                context.EmitNullableExpression(local, ctx =>
                {
                    converter(ctx);
                    ctx.Emit(OpCodes.Newobj, reflectingTargetType.GetConstructors()[0]);
                    ctx.Emit(OpCodes.Stloc, target);
                }, ctx =>
                {
                    ctx.EmitDefault(targetType);
                    ctx.Emit(OpCodes.Stloc, target);
                });
                context.Emit(OpCodes.Ldloc, target);
                return true;
            }
            return false;
        }

        private ValueConverter FindConverter(Type sourceType, Type targetType)
        {
            var matchContext = new ConverterMatchContext(sourceType, targetType);
            matchContext.SetProperty(typeof(PrimitiveValueConverter), true);
            var converter = Container.Converters.Find(matchContext);
            if (ReferenceEquals(converter, this)) return null;
            return converter;
        }

        private Action<CompilationContext> GetConvertEmitter(Type sourceType, Type targetType)
        {
            var converter = FindConverter(sourceType, targetType);
            if (converter != null)
            {
                return context => converter.Emit(sourceType, targetType, context);
            }
            if (sourceType == targetType)
            {
                return context => { };
            }
#if NetCore
            var reflectingTargetType = targetType.GetTypeInfo();
            var reflectingSourceType = sourceType.GetTypeInfo();
#else
            var reflectingTargetType = targetType;
            var reflectingSourceType = sourceType;
#endif
            if (reflectingTargetType.IsAssignableFrom(sourceType))
            {
                return context => context.EmitCast(targetType);
            }
            var convertMethod = ReflectionHelper.GetConvertMethod(sourceType, targetType);
            if (sourceType == typeof(double) && targetType == typeof(decimal))
            {
                return context =>
                {
                    context.Emit(OpCodes.Conv_R8);
                    context.EmitCall(convertMethod);
                    context.CurrentType = targetType;
                };
            }
            if (sourceType == typeof(float) && targetType == typeof(decimal))
            {
                return context =>
                {
                    context.Emit(OpCodes.Conv_R4);
                    context.EmitCall(convertMethod);
                    context.CurrentType = targetType;
                };
            }
            if (sourceType == typeof(decimal) && targetType == typeof(double))
            {
                return context =>
                {
                    context.EmitCall(convertMethod);
                    context.Emit(OpCodes.Conv_R8);
                    context.CurrentType = targetType;
                };
            }
            if (sourceType == typeof(decimal) && targetType == typeof(float))
            {
                return context =>
                {
                    context.EmitCall(convertMethod);
                    context.Emit(OpCodes.Conv_R4);
                    context.CurrentType = targetType;
                };
            }
            if (convertMethod != null)
            {
                return context =>
                {
                    context.EmitCall(convertMethod);
                    context.CurrentType = targetType;
                };
            }
            if (targetType == typeof(short))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(sbyte))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(char) || sourceType == typeof(double) || sourceType == typeof(int) ||
                    sourceType == typeof(long) || sourceType == typeof(float) || sourceType == typeof(ushort) ||
                    sourceType == typeof(uint) || sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_I2);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(ushort))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(char))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(double) || sourceType == typeof(int) || sourceType == typeof(short) ||
                    sourceType == typeof(long) || sourceType == typeof(float) || sourceType == typeof(sbyte) ||
                    sourceType == typeof(uint) || sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U2);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(int))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(sbyte) || sourceType == typeof(short) ||
                    sourceType == typeof(ushort) || sourceType == typeof(char) || sourceType == typeof(uint))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(double) || sourceType == typeof(long) || sourceType == typeof(float) ||
                    sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_I4);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(uint))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(int) || sourceType == typeof(short) ||
                    sourceType == typeof(sbyte) || sourceType == typeof(ushort) || sourceType == typeof(char))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(double) || sourceType == typeof(float) || sourceType == typeof(long) ||
                    sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U4);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(long))
            {
                if (sourceType == typeof(ulong))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(byte) || sourceType == typeof(char) || sourceType == typeof(ushort) ||
                    sourceType == typeof(uint))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U8);
                        context.CurrentType = targetType;
                    };
                }
                if (sourceType == typeof(double) || sourceType == typeof(short) || sourceType == typeof(int) ||
                    sourceType == typeof(sbyte) || sourceType == typeof(float))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_I8);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(ulong))
            {
                if (sourceType == typeof(long))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(byte) || sourceType == typeof(char) || sourceType == typeof(double) ||
                    sourceType == typeof(ushort) || sourceType == typeof(uint) || sourceType == typeof(float))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U8);
                        context.CurrentType = targetType;
                    };
                }
                if (sourceType == typeof(short) || sourceType == typeof(int) || sourceType == typeof(sbyte))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_I8);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(float))
            {
                if (sourceType == typeof(uint) || sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_R_Un);
                        context.Emit(OpCodes.Conv_R4);
                        context.CurrentType = targetType;
                    };
                }
                if (sourceType == typeof(byte) || sourceType == typeof(char) || sourceType == typeof(double) ||
                    sourceType == typeof(short) || sourceType == typeof(int) || sourceType == typeof(long) ||
                    sourceType == typeof(sbyte) || sourceType == typeof(ushort))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_R4);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(double))
            {
                if (sourceType == typeof(uint) || sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_R_Un);
                        context.Emit(OpCodes.Conv_R8);
                        context.CurrentType = targetType;
                    };
                }
                if (sourceType == typeof(byte) || sourceType == typeof(char) || sourceType == typeof(float) ||
                    sourceType == typeof(short) || sourceType == typeof(int) || sourceType == typeof(long) ||
                    sourceType == typeof(sbyte) || sourceType == typeof(ushort))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_R8);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(sbyte))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(char) || sourceType == typeof(float) ||
                    sourceType == typeof(short) || sourceType == typeof(int) || sourceType == typeof(long) ||
                    sourceType == typeof(double) || sourceType == typeof(ushort) || sourceType == typeof(uint) ||
                    sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_I1);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(byte))
            {
                if (sourceType == typeof(sbyte) || sourceType == typeof(char) || sourceType == typeof(float) ||
                    sourceType == typeof(short) || sourceType == typeof(int) || sourceType == typeof(long) ||
                    sourceType == typeof(double) || sourceType == typeof(ushort) || sourceType == typeof(uint) ||
                    sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U1);
                        context.CurrentType = targetType;
                    };
                }
            }
            if (targetType == typeof(char))
            {
                if (sourceType == typeof(byte) || sourceType == typeof(ushort))
                {
                    return context => context.CurrentType = targetType;
                }
                if (sourceType == typeof(sbyte) || sourceType == typeof(float) || sourceType == typeof(short) ||
                    sourceType == typeof(int) || sourceType == typeof(long) || sourceType == typeof(double) ||
                    sourceType == typeof(uint) || sourceType == typeof(ulong))
                {
                    return context =>
                    {
                        context.Emit(OpCodes.Conv_U2);
                        context.CurrentType = targetType;
                    };
                }
            }
            var hasEnumeration = false;
            if (reflectingSourceType.IsEnum)
            {
                sourceType = Enum.GetUnderlyingType(sourceType);
                hasEnumeration = true;
            }
            if (reflectingTargetType.IsEnum)
            {
                targetType = Enum.GetUnderlyingType(targetType);
                hasEnumeration = true;
            }
            if (hasEnumeration)
            {
                return GetConvertEmitter(sourceType, targetType);
            }
            return null;
        }
    }
}
