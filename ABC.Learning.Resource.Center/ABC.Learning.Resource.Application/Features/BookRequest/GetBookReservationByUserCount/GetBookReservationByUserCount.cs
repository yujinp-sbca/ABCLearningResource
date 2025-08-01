using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class GetBookReservationByUserCount : IGetBookReservationByUserCount
    {
        private readonly IBookRequestTransactionRepository _bookReservationTransactionRepository;
        private readonly ILogger<IGetBookReservationByUserCount> _logger;

        public GetBookReservationByUserCount(IBookRequestTransactionRepository bookReservationTransactionRepository, ILogger<IGetBookReservationByUserCount> logger) { 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookReservationTransactionRepository = bookReservationTransactionRepository ?? throw new ArgumentNullException(nameof(bookReservationTransactionRepository));
        }

        public async Task<int> Handle(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                _logger.LogError("UserId cannot be empty or null.");
                throw new ArgumentNullException(nameof(userId));
            }

            return await _bookReservationTransactionRepository.GetBookReservationByUserCount(userId);
        }
    }
}
