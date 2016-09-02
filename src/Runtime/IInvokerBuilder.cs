using System.Reflection.Emit;

namespace PowerMapper
{
    internal interface IInvokerBuilder
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
