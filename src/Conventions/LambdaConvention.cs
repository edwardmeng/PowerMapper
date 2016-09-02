using System;

namespace PowerMapper
{
    internal sealed class LambdaConvention : IConvention
    {
        private readonly Action<ConventionContext> _action;

        public LambdaConvention(Action<ConventionContext> action)
        {
            _action = action;
        }
        public void Apply(ConventionContext context)
        {
            _action?.Invoke(context);
        }

        void IConvention.SetReadOnly()
        {
        }
    }
}
