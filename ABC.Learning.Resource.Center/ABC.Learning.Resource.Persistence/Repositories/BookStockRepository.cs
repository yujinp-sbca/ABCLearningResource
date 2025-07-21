using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BookStockRepository: BaseRepository<BookStock>, IBookStockRepository
    {
        public BookStockRepository(ABCLearningResourceContext context, ILogger<BookStockRepository> logger)
            : base(context, logger)
        { }
    }
}
