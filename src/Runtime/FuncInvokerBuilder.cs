using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class FuncInvokerBuilder<T, TResult> : IInvokerBuilder
    {
        private readonly Func<T, TResult> _func;
        private MethodInfo _invokeMethod;
        private static readonly MethodInfo _funcInvokeMethod;

        static FuncInvokerBuilder()
        {
#if NETSTANDARD
            _funcInvokeMethod = typeof(Func<T, TResult>).GetTypeInfo().GetMethod("Invoke");
#else
            _funcInvokeMethod = typeof(Func<T, TResult>).GetMethod("Invoke");
#endif
        }

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
            il.Emit(OpCodes.Callvirt, _funcInvokeMethod);
            il.Emit(OpCodes.Ret);
#if NETSTANDARD
            var type = typeBuilder.CreateTypeInfo();
#else
            var type = typeBuilder.CreateType();
#endif
            type.GetField("Target").SetValue(null, _func);
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
