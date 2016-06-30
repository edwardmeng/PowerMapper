using System;
using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal class ConventionCreator<TTarget> : IInstanceCreator<TTarget>
    {
        private readonly Func<Type, object> _creator;
        private FuncInvokerBuilder<Type, object> _invokerBuilder;

        public ConventionCreator(Func<Type, object> creator)
        {
            _creator = creator;
        }

        public void Compile(ModuleBuilder builder)
        {
            if (_invokerBuilder == null)
            {
                _invokerBuilder = new FuncInvokerBuilder<Type, object>(_creator);
                _invokerBuilder.Compile(builder);
            }
        }

        public void Emit(CompilationContext context)
        {
            context.EmitTypeOf(typeof(TTarget));
            context.CurrentType = typeof(Type);
            _invokerBuilder.Emit(context);
            context.EmitCast(typeof(TTarget));
        }
    }
}
