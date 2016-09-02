using Wheatech.Activation;

[assembly:AssemblyActivator(typeof(PowerMapper.MapperActivator))]

namespace PowerMapper
{
    internal class MapperActivator
    {
        public MapperActivator(IActivatingEnvironment environment)
        {
            environment.Use(Mapper.Default);
        }
    }
}
