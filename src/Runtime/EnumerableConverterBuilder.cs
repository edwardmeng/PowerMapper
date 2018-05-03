using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class EnumerableConverterBuilder<TSource, TTarget> : IInvokerBuilder
    {
        private readonly MappingContainer _container;
        private MethodInfo _invokeMethod;
        private static readonly MethodInfo _toArrayMethod;

        static EnumerableConverterBuilder()
        {
#if NETSTANDARD
            _toArrayMethod = typeof(Enumerable).GetTypeInfo().GetMethod("ToArray").MakeGenericMethod(typeof(TSource));
#else
            _toArrayMethod = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(typeof(TSource));
#endif
        }

        public EnumerableConverterBuilder(MappingContainer container)
        {
            _container = container;
        }

        public void Compile(ModuleBuilder builder)
        {
            var invokerBuilder = new FuncInvokerBuilder<TSource, TTarget>(_container.GetMapFunc<TSource, TTarget>());
            invokerBuilder.Compile(builder);

            var typeBuilder = builder.DefineStaticType();
            var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");
            methodBuilder.SetParameters(typeof(IEnumerable<TSource>));
            methodBuilder.SetReturnType(typeof(IEnumerable<TTarget>));

            var il = methodBuilder.GetILGenerator();

            var labelReturn = il.DefineLabel();
            var labelNull = il.DefineLabel();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Brfalse, labelNull);

            var sourceArray = il.DeclareLocal(typeof(TSource[]));
            var targetArray = il.DeclareLocal(typeof(TTarget[]));
            var index = il.DeclareLocal(typeof(int));

            // Convert parameter to array.
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, _toArrayMethod);
            il.Emit(OpCodes.Stloc, sourceArray);

            // Declare new array of target.
            il.Emit(OpCodes.Ldloc, sourceArray);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Conv_I4);
            il.Emit(OpCodes.Newarr, typeof(TTarget));
            il.Emit(OpCodes.Stloc, targetArray);

            // var i = 0;
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc, index);

            var labelEnd = il.DefineLabel();
            il.Emit(OpCodes.Br_S, labelEnd);
            var labelStart = il.DefineLabel();
            il.MarkLabel(labelStart);

            // targetArray[i] = convert(sourceArray[i]);


            il.Emit(OpCodes.Ldloc, targetArray);
            il.Emit(OpCodes.Ldloc, index);

            il.Emit(OpCodes.Ldloc, sourceArray);
            il.Emit(OpCodes.Ldloc, index);
            il.Emit(OpCodes.Ldelem, typeof(TSource));
            il.Emit(OpCodes.Call, invokerBuilder.MethodInfo);
            il.Emit(OpCodes.Stelem, typeof(TTarget));

            // i++
            il.Emit(OpCodes.Ldloc, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc, index);


            il.MarkLabel(labelEnd);
            il.Emit(OpCodes.Ldloc, index);
            il.Emit(OpCodes.Ldloc, sourceArray);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Conv_I4);
            il.Emit(OpCodes.Blt_S, labelStart);

            il.Emit(OpCodes.Ldloc, targetArray);
            il.Emit(OpCodes.Castclass, typeof(IEnumerable<TTarget>));
            il.Emit(OpCodes.Br_S, labelReturn);

            il.MarkLabel(labelNull);
            il.Emit(OpCodes.Ldnull);
            il.MarkLabel(labelReturn);
            il.Emit(OpCodes.Ret);

#if NETSTANDARD
            var type = typeBuilder.CreateTypeInfo();
#else
            var type = typeBuilder.CreateType();
#endif
            _invokeMethod = type.GetMethod("Invoke");
        }

        public void Emit(CompilationContext context)
        {
            context.EmitCall(_invokeMethod);
            context.CurrentType = typeof(IEnumerable<TTarget>);
        }
    }
}
