using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
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
#if NETSTANDARD
            var invokeMethod = typeof(Func<T, TResult>).GetTypeInfo().GetMethod("Invoke");
#else
            var invokeMethod = typeof(Func<T, TResult>).GetMethod("Invoke");
#endif
            il.Emit(OpCodes.Callvirt, invokeMethod);
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
