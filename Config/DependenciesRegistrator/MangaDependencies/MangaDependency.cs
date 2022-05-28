using Domain.Contracts.Filters;
using Domain.Contracts.Filters.Models;
using Data.Filters;
using Data.Repositories;
using Domain.Entities;
using Domain.Repositories;

namespace Host.Config.DependenciesRegistrator.MangaDependencies
{
    public static class MangaDependency
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain
            services.AddTransient<IMangaRepository, EFMangaRepository>();

            // Data
            services.AddTransient<IEntityFilter<Manga, IMangaFilterParameters>, MangaFilter>();
        }
    }
}
