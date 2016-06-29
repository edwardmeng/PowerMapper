using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Wheatech.EmitMapper.Runtime;

namespace Wheatech.EmitMapper
{
    internal class EnumerableMapper: ValueMapper
    {
        private readonly MappingContainer _container;
        private readonly Type _sourceElementType;
        private readonly Type _targetElementType;
        private IInvokerBuilder _invokerBuilder;

        public EnumerableMapper(MappingContainer container, Type sourceElementType, Type targetElementType)
        {
            _container = container;
            _sourceElementType = sourceElementType;
            _targetElementType = targetElementType;
        }

        public override void Compile(ModuleBuilder builder)
        {
            _invokerBuilder =
                (IInvokerBuilder)
                    Activator.CreateInstance(typeof(EnumerableMapperBuilder<,>).MakeGenericType(_sourceElementType, _targetElementType), _container);
            _invokerBuilder.Compile(builder);
        }

        public override void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
            context.LoadSource(LoadPurpose.Parameter);
            context.CurrentType = sourceType;
            context.EmitCast(typeof(IEnumerable<>).MakeGenericType(_sourceElementType));
            context.LoadTarget(LoadPurpose.Parameter);
            context.CurrentType = targetType;
            context.EmitCast(typeof(IEnumerable<>).MakeGenericType(_targetElementType));
            _invokerBuilder.Emit(context);
        }
    }
}
