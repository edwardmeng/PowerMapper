using System.Reflection.Emit;

namespace PowerMapper
{
    internal interface IInstanceCreator<TTarget>
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
