using Microsoft.EntityFrameworkCore;
using ABC.Learning.Resource.Persistence;

namespace ABC.Learning.Resource.API.Extensions
{
    public static class ContextDBCollectionExtension
    {
        public static IServiceCollection RegisterABCLearningDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Database:ConnectionString"];

            services.AddDbContextPool<ABCLearningResourceContext>(options =>
                options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string not found in environment variables.")));

            return services;
        }
    }
}
