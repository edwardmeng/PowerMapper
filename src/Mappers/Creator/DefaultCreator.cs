using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using PowerMapper.Properties;

namespace PowerMapper
{
    internal class DefaultCreator<TTarget> : IInstanceCreator<TTarget>
    {
        public void Compile(ModuleBuilder builder)
        {
        }

        public void Emit(CompilationContext context)
        {
#if NetCore
            var reflectingTargetType = typeof(TTarget).GetTypeInfo();
#else
            var reflectingTargetType = typeof(TTarget);
#endif
            if (reflectingTargetType.IsValueType || typeof(TTarget).IsNullable())
            {
                var targetLocal = context.DeclareLocal(typeof(TTarget));
                context.Emit(OpCodes.Ldloca, targetLocal);
                context.Emit(OpCodes.Initobj, typeof(TTarget));
                context.Emit(OpCodes.Ldloc, targetLocal);
            }
            else
            {
                var constructor =
                    reflectingTargetType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                        .FirstOrDefault(ctor => ctor.GetParameters().Length == 0);
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
