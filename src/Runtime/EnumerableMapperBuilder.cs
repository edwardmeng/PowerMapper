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
            il.Emit(OpCodes.Callvirt,typeof(IEnumerable<TSource>).GetMethod("GetEnumerator"));
            il.Emit(OpCodes.Stloc, sourceEnumerator);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, typeof(IEnumerable<TTarget>).GetMethod("GetEnumerator"));
            il.Emit(OpCodes.Stloc, targetEnumerator);

            var checkLabel = il.DefineLabel();
            var startLabel = il.DefineLabel();
            var endLabel = il.DefineLabel();

            il.Emit(OpCodes.Br_S, checkLabel);
            il.MarkLabel(startLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, typeof(IEnumerator<TSource>).GetMethod("get_Current"));

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, typeof(IEnumerator<TTarget>).GetMethod("get_Current"));

            il.Emit(OpCodes.Call, invokerBuilder.MethodInfo);

            il.MarkLabel(checkLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));

            il.Emit(OpCodes.Brfalse_S, endLabel);

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, typeof(IEnumerator).GetMethod("MoveNext"));
            il.Emit(OpCodes.Brtrue_S, startLabel);

            il.MarkLabel(endLabel);
            il.Emit(OpCodes.Ret);

            var type = typeBuilder.CreateType();
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
        }
    }
}
