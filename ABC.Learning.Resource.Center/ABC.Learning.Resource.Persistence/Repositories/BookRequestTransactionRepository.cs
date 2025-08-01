using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BookRequestTransactionRepository : BaseRepository<BookRequestTransaction>, IBookRequestTransactionRepository
    {
        public BookRequestTransactionRepository(ABCLearningResourceContext context, ILogger<BookRequestTransactionRepository> logger) : base(context, logger)
        {
            
        }

        public async Task<int> GetBookReservationByUserCount(Guid userId)
        {
            var bookReservations = _context.BookReservationTransactions
                        .Where(br => br.CreatedBy.Equals(userId) && br.IsActive)
                        .ToList();

            return bookReservations.Count();
                        
        }
    }
}
