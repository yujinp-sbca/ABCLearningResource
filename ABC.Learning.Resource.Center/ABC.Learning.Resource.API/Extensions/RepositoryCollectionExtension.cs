using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Persistence.Repositories;

namespace ABC.Learning.Resource.API.Extensions
{
    public static class RepositoryCollectionExtension
    {
        public static IServiceCollection RegisterABCLearningResourceContexts(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
