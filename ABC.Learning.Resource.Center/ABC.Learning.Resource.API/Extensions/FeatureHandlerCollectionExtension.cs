
using ABC.Learning.Resource.Application.Features.Books;
using ABC.Learning.Resource.Application.Features.Books.DeleteBookById;
using ABC.Learning.Resource.Application.Features.User;

namespace ABC.Learning.Resource.API.Extensions
{
    public static class FeatureHandlerCollectionExtension
    {
        public static IServiceCollection RegisterABCLearningResourceContexts(this IServiceCollection services)
        {
            services.AddScoped<IGetBookByIdHandler, GetBookByIdHandler>();
            services.AddScoped<IAddBookHandler, AddBookHandler>();
            services.AddScoped<IAddBookPriceHandler, AddBookPriceHandler>();
            services.AddScoped<IAddBookStockHandler, AddBookStockHandler>();
            services.AddScoped<IUpdateBookHandler, UpdateBookHandler>();
            services.AddScoped<IDeleteBookByIdHandler, DeleteBookByIdHandler>();
            services.AddScoped<IGetBookPriceByBookId, GetBookPriceByBookId>();
            services.AddScoped<IGetBookStockByBookId, GetBookStockByBookId>();
            services.AddScoped<IGetActiveUserByEmailHandler, GetActiveUserByEmailHandler>();

            return services;
        }
    }
}
