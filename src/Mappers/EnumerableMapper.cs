using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Wheatech.ObjectMapper.Runtime;

namespace Wheatech.ObjectMapper
{
    internal class EnumerableMapper
    {
        private readonly ObjectMapper _container;
        private readonly Type _sourceElementType;
        private readonly Type _targetElementType;
        private IInvokerBuilder _invokerBuilder;

        public EnumerableMapper(ObjectMapper container, Type sourceElementType, Type targetElementType)
        {
            _container = container;
            _sourceElementType = sourceElementType;
            _targetElementType = targetElementType;
        }

        public void Compile(ModuleBuilder builder)
        {
            _invokerBuilder =
                (IInvokerBuilder)
                    Activator.CreateInstance(typeof(EnumerableMapperBuilder<,>).MakeGenericType(_sourceElementType, _targetElementType), _container);
            _invokerBuilder.Compile(builder);
        }

        public void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
            context.LoadSource(LoadPurpose.Parameter);
            context.CurrentType = sourceType;
            context.EmitCast(typeof(IEnumerable<>).MakeGenericType(_sourceElementType));
            context.LoadTarget(LoadPurpose.Parameter);
            context.CurrentType = targetType;
            context.EmitCast(typeof(IEnumerable<>).MakeGenericType(_targetElementType));
            _invokerBuilder.Emit(context);
        }

        public virtual Delegate CreateDelegate(Type sourceType, Type targetType, ModuleBuilder builder)
        {
            var typeBuilder = builder.DefineStaticType();
            var methodBuilder = typeBuilder.DefineStaticMethod("Map");
            methodBuilder.SetParameters(sourceType, targetType);
            var il = methodBuilder.GetILGenerator();
            var context = new CompilationContext(il);
            context.SetSource(purpose => il.Emit(OpCodes.Ldarg_0));
            context.SetTarget(purpose => il.Emit(OpCodes.Ldarg_1));
            Emit(sourceType, targetType, context);
            context.Emit(OpCodes.Ret);
            var type = typeBuilder.CreateType();
            return Delegate.CreateDelegate(typeof(Action<,>).MakeGenericType(sourceType, targetType), type, "Map");
        }
    }
}
