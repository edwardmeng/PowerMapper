using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal class ActionInvokerBuilder<TSource, TTarget> : IInvokerBuilder
    {
        private readonly Action<TSource, TTarget> _action;
        private MethodInfo _invokeMethod;

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
            il.Emit(OpCodes.Callvirt, typeof(Action<TSource, TTarget>).GetMethod("Invoke"));
            il.Emit(OpCodes.Ret);
            var type = typeBuilder.CreateType();
            Helper.SetStaticField(type, "Target", _action);
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
            context.CurrentType = null;
        }
    }
}
