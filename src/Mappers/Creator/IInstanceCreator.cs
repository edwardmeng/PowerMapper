using System.Reflection.Emit;

namespace Wheatech.EmitMapper
{
    internal interface IInstanceCreator<TTarget>
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
