using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class ActionInvokerBuilder<TSource, TTarget> : IInvokerBuilder
    {
        private readonly Action<TSource, TTarget> _action;
        private MethodInfo _invokeMethod;
        private static readonly MethodInfo _actionInvokeMethod;

        static ActionInvokerBuilder()
        {
#if NetCore
            _actionInvokeMethod = typeof(Action<TSource, TTarget>).GetTypeInfo().GetMethod("Invoke");
#else
            _actionInvokeMethod = typeof(Action<TSource, TTarget>).GetMethod("Invoke");
#endif
        }

        public ActionInvokerBuilder(Action<TSource, TTarget> action)
        {
            _action = action;
        }

        public MethodInfo MethodInfo => _invokeMethod;

        public void Compile(ModuleBuilder builder)
        {
            var typeBuilder = builder.DefineStaticType();
            var field = typeBuilder.DefineStaticField<Action<TSource, TTarget>>("Target");
            var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");
            methodBuilder.SetParameters(typeof(TSource), typeof(TTarget));

            var il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldsfld, field);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, _actionInvokeMethod);
            il.Emit(OpCodes.Ret);
#if NetCore
            var type = typeBuilder.CreateTypeInfo();
#else
            var type = typeBuilder.CreateType();
#endif
            type.GetField("Target").SetValue(null, _action);
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
            context.CurrentType = null;
        }
    }
}
