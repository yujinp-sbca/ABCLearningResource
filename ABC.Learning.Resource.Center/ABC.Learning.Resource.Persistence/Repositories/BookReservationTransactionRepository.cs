using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BookReservationTransactionRepository : BaseRepository<BookReservationTransaction>, IBookReservationTransactionRepository
    {
        public BookReservationTransactionRepository(ABCLearningResourceContext context, ILogger<BaseRepository<BookReservationTransaction>> logger) : base(context, logger)
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
