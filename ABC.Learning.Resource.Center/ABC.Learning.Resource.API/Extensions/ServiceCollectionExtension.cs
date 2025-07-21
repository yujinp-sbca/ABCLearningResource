
using ABC.Learning.Resource.Application.Services;

namespace ABC.Learning.Resource.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterABCLearningResourceServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();           

            return services;
        }
    }
}
