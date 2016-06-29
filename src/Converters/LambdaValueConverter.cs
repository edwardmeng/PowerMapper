using System;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal class LambdaValueConverter<TSource, TTarget> : ValueConverter
    {
        private readonly Func<TSource, TTarget> _expression;
        private FuncInvokerBuilder<TSource, TTarget> _invokerBuilder;

        public LambdaValueConverter(Func<TSource, TTarget> expression)
        {
            _expression = expression;
        }

        public override int Match(ConverterMatchContext context)
        {
            var sourceDistance = Helper.GetDistance(context.SourceType, typeof(TSource));
            var targetDistance = Helper.GetDistance(context.TargetType, typeof(TTarget));
            return sourceDistance == -1 || targetDistance == -1 ? -1 : sourceDistance + targetDistance;
        }

        public override void Compile(ModuleBuilder builder)
        {
            if (_invokerBuilder == null)
            {
                _invokerBuilder = new FuncInvokerBuilder<TSource, TTarget>(_expression);
                _invokerBuilder.Compile(builder);
            }
        }

        public override void Emit(Type sourceType, Type targetType, CompilationContext context)
        {
            if (typeof(TSource) != sourceType)
            {
                context.EmitCast(typeof(TSource));
            }
            _invokerBuilder.Emit(context);
            if (targetType != typeof(TTarget))
            {
                context.EmitCast(targetType);
            }
            context.CurrentType = targetType;
        }
    }
}
