using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal class ObjectToStringConverter : Converter
    {
        private static readonly MethodInfo _toStringMethod = typeof(object).GetMethod("ToString",
            BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        public override int Match(ConverterMatchContext context)
        {
            return context.TargetType == typeof(string) ? Helper.GetDistance(context.SourceType, typeof(object)) : -1;
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
            if (Helper.IsNullable(sourceType))
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
            else if (sourceType.IsValueType)
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
