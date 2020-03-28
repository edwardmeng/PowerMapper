using System;
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

        private void EmitConverter(ILGenerator il, MethodInfo elementConverter, Action loadSource)
        {
            var labelReturn = il.DefineLabel();
            var labelNull = il.DefineLabel();

            loadSource();
            //il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Brfalse, labelNull);

            var sourceArray = il.DeclareLocal(typeof(TSource[]));
            var targetArray = il.DeclareLocal(typeof(TTarget[]));
            var index = il.DeclareLocal(typeof(int));

            // Convert parameter to array.
            loadSource();
            //il.Emit(OpCodes.Ldarg_0);
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
            il.Emit(OpCodes.Call, elementConverter);
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
        }

        public void Compile(ModuleBuilder builder)
        {
            if (!TypeMapper<TSource, TTarget>.TryGetInstance(_container, out var mapper))
            {
                var invokerBuilder = new FuncInvokerBuilder<TSource, TTarget>(_container.GetMapFunc<TSource, TTarget>());
                invokerBuilder.Compile(builder);

                var typeBuilder = builder.DefineStaticType();
                var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");
                methodBuilder.SetParameters(typeof(IEnumerable<TSource>));
                methodBuilder.SetReturnType(typeof(IEnumerable<TTarget>));

                var il = methodBuilder.GetILGenerator();
                EmitConverter(il, invokerBuilder.MethodInfo, () => il.Emit(OpCodes.Ldarg_0));
                il.Emit(OpCodes.Ret);

#if NETSTANDARD
                var type = typeBuilder.CreateTypeInfo();
#else
                var type = typeBuilder.CreateType();
#endif
                _invokeMethod = type.GetMethod("Invoke");
            }
            else if (mapper.ConverterMethod == null)
            {
                mapper.CreateConverter(builder);
            }
        }

        public void Emit(CompilationContext context)
        {
            if (_invokeMethod != null)
            {
                context.EmitCall(_invokeMethod);
            }
            else if (TypeMapper<TSource, TTarget>.TryGetInstance(_container, out var mapper))
            {
                var sourceLocal = context.DeclareLocal(typeof(IEnumerable<TSource>));
                context.EmitCast(typeof(IEnumerable<TSource>));
                context.ILGenerator.Emit(OpCodes.Stloc, sourceLocal);
                EmitConverter(context.ILGenerator, mapper.ConverterMethod,
                    () => context.ILGenerator.Emit(OpCodes.Ldloc, sourceLocal));
            }
            context.CurrentType = typeof(IEnumerable<TTarget>);
        }
    }
}
