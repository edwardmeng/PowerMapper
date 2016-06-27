using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal interface IInvokerBuilder
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
