using Wheatech.Activation;

[assembly:AssemblyActivator(typeof(Wheatech.EmitMapper.MapperActivator))]

namespace Wheatech.EmitMapper
{
    internal class MapperActivator
    {
        public MapperActivator(IActivatingEnvironment environment)
        {
            environment.Use(Mapper.Default);
        }
    }
}
