using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal interface IInvokerBuilder
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
