using ABC.Learning.Resource.Application.Services.Book;
using ABC.Learning.Resource.Application.Services.Book.Models;

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
