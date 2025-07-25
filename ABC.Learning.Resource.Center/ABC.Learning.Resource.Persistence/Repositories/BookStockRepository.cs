using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BookStockRepository: BaseRepository<BookStock>, IBookStockRepository
    {
        public BookStockRepository(ABCLearningResourceContext context, ILogger<BookStockRepository> logger)
            : base(context, logger)
        { }

        public async Task<BookStock> GetByBookIdAsync(Guid bookId)
        {
            return await _context.BookStocks
                         .Where(bs => bs.BookId == bookId)
                         .FirstOrDefaultAsync();
        }
    }
}
