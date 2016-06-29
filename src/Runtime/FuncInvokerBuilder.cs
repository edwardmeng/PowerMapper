using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal class FuncInvokerBuilder<T, TResult> : IInvokerBuilder
    {
        private readonly Func<T, TResult> _func;
        private MethodInfo _invokeMethod;

        public FuncInvokerBuilder(Func<T, TResult> func)
        {
            _func = func;
        }

        public void Compile(ModuleBuilder builder)
        {
            var typeBuilder = builder.DefineStaticType();
            var field = typeBuilder.DefineStaticField<Func<T, TResult>>("Target");
            var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");
            methodBuilder.SetReturnType(typeof(TResult));
            methodBuilder.SetParameters(typeof(T));

            var il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldsfld, field);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, typeof(Func<T, TResult>).GetMethod("Invoke"));
            il.Emit(OpCodes.Ret);
            var type = typeBuilder.CreateType();
            Helper.SetStaticField(type, "Target", _func);
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
            context.CurrentType = typeof(TResult);
        }

        public MethodInfo MethodInfo => _invokeMethod;
    }
}
