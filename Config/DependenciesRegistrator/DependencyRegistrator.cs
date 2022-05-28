using Host.Config.DependenciesRegistrator.MangaDependencies;

namespace Host.Config.DependenciesRegistrator
{
    public static class DependencyRegistrator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            MangaDependency.RegisterServices(services);
        }
    }
}
