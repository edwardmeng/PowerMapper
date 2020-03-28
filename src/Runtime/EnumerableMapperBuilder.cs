using System;
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
        private static readonly MethodInfo _referenceEqualsMethod;

        static EnumerableMapperBuilder()
        {
#if NETSTANDARD
            _sourceGetEnumeratorMethod = typeof(IEnumerable<TSource>).GetTypeInfo().GetMethod("GetEnumerator");
            _targetGetEnumeratorMethod = typeof(IEnumerable<TTarget>).GetTypeInfo().GetMethod("GetEnumerator");
            _sourceGetCurrentMethod = typeof(IEnumerator<TSource>).GetTypeInfo().GetMethod("get_Current");
            _targetGetCurrentMethod = typeof(IEnumerator<TTarget>).GetTypeInfo().GetMethod("get_Current");
            _moveNextMethod = typeof(IEnumerator).GetTypeInfo().GetMethod("MoveNext");
            _referenceEqualsMethod = typeof(object).GetTypeInfo().GetMethod("ReferenceEquals");
#else
            _sourceGetEnumeratorMethod = typeof(IEnumerable<TSource>).GetMethod("GetEnumerator");
            _targetGetEnumeratorMethod = typeof(IEnumerable<TTarget>).GetMethod("GetEnumerator");
            _sourceGetCurrentMethod = typeof(IEnumerator<TSource>).GetMethod("get_Current");
            _targetGetCurrentMethod = typeof(IEnumerator<TTarget>).GetMethod("get_Current");
            _moveNextMethod = typeof(IEnumerator).GetMethod("MoveNext");
            _referenceEqualsMethod = typeof(object).GetMethod("ReferenceEquals");
#endif
        }

        public EnumerableMapperBuilder(MappingContainer container)
        {
            _container = container;
        }

        private void EmitMapper(ILGenerator il, MethodInfo elementMapper, Action loadSource, Action loadTarget)
        {
            var sourceEnumerator = il.DeclareLocal(typeof(IEnumerator<TSource>));
            var targetEnumerator = il.DeclareLocal(typeof(IEnumerator<TTarget>));

            var checkLabel = il.DefineLabel();
            var startLabel = il.DefineLabel();
            var endLabel = il.DefineLabel();

            loadSource();
            //il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Call, _referenceEqualsMethod);
            il.Emit(OpCodes.Brtrue, endLabel);

            loadTarget();
            //il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Call, _referenceEqualsMethod);
            il.Emit(OpCodes.Brtrue, endLabel);

            loadSource();
            //il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, _sourceGetEnumeratorMethod);
            il.Emit(OpCodes.Stloc, sourceEnumerator);

            loadTarget();
            //il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, _targetGetEnumeratorMethod);
            il.Emit(OpCodes.Stloc, targetEnumerator);

            il.Emit(OpCodes.Br_S, checkLabel);
            il.MarkLabel(startLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, _sourceGetCurrentMethod);

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, _targetGetCurrentMethod);

            il.Emit(OpCodes.Call, elementMapper);

            il.MarkLabel(checkLabel);

            il.Emit(OpCodes.Ldloc, sourceEnumerator);
            il.Emit(OpCodes.Callvirt, _moveNextMethod);

            il.Emit(OpCodes.Brfalse_S, endLabel);

            il.Emit(OpCodes.Ldloc, targetEnumerator);
            il.Emit(OpCodes.Callvirt, _moveNextMethod);
            il.Emit(OpCodes.Brtrue_S, startLabel);

            il.MarkLabel(endLabel);
        }

        public void Compile(ModuleBuilder builder)
        {
            if (!TypeMapper<TSource, TTarget>.TryGetInstance(_container, out var mapper))
            {
                var invokerBuilder = new ActionInvokerBuilder<TSource, TTarget>(_container.GetMapAction<TSource, TTarget>());
                invokerBuilder.Compile(builder);

                var typeBuilder = builder.DefineStaticType();
                var methodBuilder = typeBuilder.DefineStaticMethod("Invoke");

                methodBuilder.SetParameters(typeof(IEnumerable<TSource>), typeof(IEnumerable<TTarget>));
                var il = methodBuilder.GetILGenerator();

                EmitMapper(il, invokerBuilder.MethodInfo,
                    () => il.Emit(OpCodes.Ldarg_0),
                    () => il.Emit(OpCodes.Ldarg_1));
                il.Emit(OpCodes.Ret);

#if NETSTANDARD
                var type = typeBuilder.CreateTypeInfo();
#else
                var type = typeBuilder.CreateType();
#endif
                _invokeMethod = type.GetMethod("Invoke");
            }
            else if (mapper.MapperMethod == null)
            {
                mapper.CreateMapper(builder);
            }
        }

        public void Emit(CompilationContext context)
        {
            if (_invokeMethod != null)
            {
                context.EmitCall(_invokeMethod);
            }
            else if(TypeMapper<TSource, TTarget>.TryGetInstance(_container, out var mapper))
            {
                var sourceLocal = context.DeclareLocal(typeof(IEnumerable<TSource>));
                var targetLocal = context.DeclareLocal(typeof(IEnumerable<TTarget>));
                context.EmitCast(typeof(IEnumerable<TTarget>));
                context.ILGenerator.Emit(OpCodes.Stloc, targetLocal);
                context.EmitCast(typeof(IEnumerable<TSource>));
                context.ILGenerator.Emit(OpCodes.Stloc, sourceLocal);
                EmitMapper(context.ILGenerator, mapper.MapperMethod,
                    () => context.ILGenerator.Emit(OpCodes.Ldloc, sourceLocal),
                    () => context.ILGenerator.Emit(OpCodes.Ldloc, targetLocal));
            }
        }
    }
}
