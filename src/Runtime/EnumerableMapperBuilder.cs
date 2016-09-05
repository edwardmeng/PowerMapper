using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper.Runtime
{
    internal class EnumerableMapperBuilder<TSource, TTarget> : IInvokerBuilder
    {
        private readonly MappingContainer _container;
        private MethodInfo _invokeMethod;
        private static readonly MethodInfo _sourceGetEnumeratorMethod;
        private static readonly MethodInfo _targetGetEnumeratorMethod;
        private static readonly MethodInfo _sourceGetCurrentMethod;
        private static readonly MethodInfo _targetGetCurrentMethod;
        private static readonly MethodInfo _moveNextMethod;

        static EnumerableMapperBuilder()
        {
#if NetCore
            _sourceGetEnumeratorMethod = typeof(IEnumerable<TSource>).GetTypeInfo().GetMethod("GetEnumerator");
            _targetGetEnumeratorMethod = typeof(IEnumerable<TTarget>).GetTypeInfo().GetMethod("GetEnumerator");
            _sourceGetCurrentMethod = typeof(IEnumerator<TSource>).GetTypeInfo().GetMethod("get_Current");
            _targetGetCurrentMethod = typeof(IEnumerator<TTarget>).GetTypeInfo().GetMethod("get_Current");
            _moveNextMethod = typeof(IEnumerator).GetTypeInfo().GetMethod("MoveNext");
#else
            _sourceGetEnumeratorMethod = typeof(IEnumerable<TSource>).GetMethod("GetEnumerator");
            _targetGetEnumeratorMethod = typeof(IEnumerable<TTarget>).GetMethod("GetEnumerator");
            _sourceGetCurrentMethod = typeof(IEnumerator<TSource>).GetMethod("get_Current");
            _targetGetCurrentMethod = typeof(IEnumerator<TTarget>).GetMethod("get_Current");
            _moveNextMethod = typeof(IEnumerator).GetMethod("MoveNext");
#endif
        }

        public EnumerableMapperBuilder(MappingContainer container)
        {
            _container = container;
        }

        public void Compile(ModuleBuilder builder)
        {
            var invokerBuilder = new ActionInvokerBuilder<TSource, TTarget>(_container.GetMapAction<TSource, TTarget>());
            invokerBuilder.Compile(builder);

            var typeBuilder = builder.DefineStaticType();
            var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");

            methodBuilder.SetParameters(typeof(IEnumerable<TSource>), typeof(IEnumerable<TTarget>));
            var il = methodBuilder.GetILGenerator();

            var sourceEnumerator = il.DeclareLocal(typeof(IEnumerator<TSource>));
            var targetEnumerator = il.DeclareLocal(typeof(IEnumerator<TTarget>));

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, _sourceGetEnumeratorMethod);
            il.Emit(OpCodes.Stloc, sourceEnumerator);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, _targetGetEnumeratorMethod);
            il.Emit(OpCodes.Stloc, targetEnumerator);

            var checkLabel = il.DefineLabel();
            var startLabel = il.DefineLabel();
            var endLabel = il.DefineLabel();

            il.Emit(OpCodes.Br_S, checkLabel);
            il.MarkLabel(startLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, _sourceGetCurrentMethod);

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, _targetGetCurrentMethod);

            il.Emit(OpCodes.Call, invokerBuilder.MethodInfo);

            il.MarkLabel(checkLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, _moveNextMethod);

            il.Emit(OpCodes.Brfalse_S, endLabel);

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, _moveNextMethod);
            il.Emit(OpCodes.Brtrue_S, startLabel);

            il.MarkLabel(endLabel);
            il.Emit(OpCodes.Ret);

#if NetCore
            var type = typeBuilder.CreateTypeInfo();
#else
            var type = typeBuilder.CreateType();
#endif
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
        }
    }
}
