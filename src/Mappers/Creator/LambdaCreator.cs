using System;
using System.Reflection.Emit;

namespace PowerMapper
{
    internal class LambdaCreator<TSource, TTarget> : IInstanceCreator<TTarget>
    {
        private readonly Func<TSource, TTarget> _expression;
        private FuncInvokerBuilder<TSource, TTarget> _invokerBuilder;

        public LambdaCreator(Func<TSource, TTarget> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            _expression = expression;
        }

        public void Compile(ModuleBuilder builder)
        {
            if (_invokerBuilder == null)
            {
                _invokerBuilder = new FuncInvokerBuilder<TSource, TTarget>(_expression);
                _invokerBuilder.Compile(builder);
            }
        }

        public void Emit(CompilationContext context)
        {
            context.LoadSource(LoadPurpose.Parameter);
            context.CurrentType = typeof(TSource);
            _invokerBuilder.Emit(context);
            context.CurrentType = typeof(TTarget);
        }
    }
}
