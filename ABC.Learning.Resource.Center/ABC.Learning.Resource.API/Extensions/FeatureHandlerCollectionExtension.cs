
using ABC.Learning.Resource.Application.Features.Books;

namespace ABC.Learning.Resource.API.Extensions
{
    public static class FeatureHandlerCollectionExtension
    {
        public static IServiceCollection RegisterABCLearningResourceContexts(this IServiceCollection services)
        {
            services.AddScoped<IAddBookHandler, AddBookHandler>();
            services.AddScoped<IAddBookPriceHandler, AddBookPriceHandler>();
            services.AddScoped<IAddBookStockHandler, AddBookStockHandler>();

            return services;
        }
    }
}
