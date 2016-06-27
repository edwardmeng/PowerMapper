using System.Reflection.Emit;

namespace Wheatech.ObjectMapper
{
    internal interface IInstanceCreator<TTarget>
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
