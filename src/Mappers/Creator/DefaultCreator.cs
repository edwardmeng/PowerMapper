using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using Wheatech.EmitMapper.Properties;

namespace Wheatech.EmitMapper
{
    internal class DefaultCreator<TTarget> : IInstanceCreator<TTarget>
    {
        public void Compile(ModuleBuilder builder)
        {
        }

        public void Emit(CompilationContext context)
        {
            if (typeof(TTarget).IsValueType || typeof(TTarget).IsNullable())
            {
                var targetLocal = context.DeclareLocal(typeof(TTarget));
                context.Emit(OpCodes.Ldloca, targetLocal);
                context.Emit(OpCodes.Initobj, typeof(TTarget));
                context.Emit(OpCodes.Ldloc, targetLocal);
            }
            else
            {
                var constructor =
                    typeof(TTarget).GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                        Type.EmptyTypes, null);
                if (constructor == null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.Creator_CannotFindConstructor, typeof(TTarget)));
                }
                context.Emit(OpCodes.Newobj, constructor);
            }
            context.CurrentType = typeof(TTarget);
        }
    }
}
