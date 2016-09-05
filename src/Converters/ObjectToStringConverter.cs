using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class ObjectToStringConverter : ValueConverter
    {
        private static readonly MethodInfo _toStringMethod;

        static ObjectToStringConverter()
        {
#if NetCore
            _toStringMethod = typeof(object).GetTypeInfo().GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance);
#else
            _toStringMethod = typeof(object).GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
#endif
        }

        public override int Match(ConverterMatchContext context)
        {
            return context.TargetType == typeof(string) ? ReflectionHelper.GetDistance(context.SourceType, typeof(object)) : -1;
        }

        public override void Compile(ModuleBuilder builder)
        {
        }

        public override void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
            if (sourceType == typeof(string))
            {
                return;
            }
            if (sourceType.IsNullable())
            {
                var target = context.DeclareLocal(targetType);
                var local = context.DeclareLocal(sourceType);
                context.Emit(OpCodes.Stloc, local);
                context.EmitNullableExpression(local, ctx =>
                {
                    ctx.EmitCast(typeof(object));
                    ctx.EmitCall(_toStringMethod);
                    ctx.Emit(OpCodes.Stloc, target);
                }, ctx =>
                {
                    ctx.EmitDefault(typeof(string));
                    ctx.Emit(OpCodes.Stloc, target);
                });
                context.Emit(OpCodes.Ldloc, target);
            }
#if NetCore
            else if (sourceType.GetTypeInfo().IsValueType)
#else
            else if (sourceType.IsValueType)
#endif
            {
                context.EmitCast(typeof(object));
                context.EmitCall(_toStringMethod);
            }
            else
            {
                var target = context.DeclareLocal(targetType);
                var local = context.DeclareLocal(sourceType);
                context.Emit(OpCodes.Stloc, local);
                context.EmitNullableExpression(local, ctx =>
                {
                    ctx.EmitCast(typeof(object));
                    ctx.EmitCall(_toStringMethod);
                    ctx.Emit(OpCodes.Stloc, target);
                }, ctx =>
                {
                    context.Emit(OpCodes.Ldnull);
                    ctx.Emit(OpCodes.Stloc, target);
                });
                context.Emit(OpCodes.Ldloc, target);
            }
        }
    }
}
