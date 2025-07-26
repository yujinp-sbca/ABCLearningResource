using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class GetBookStockByBookId : IGetBookStockByBookId
    {
        private readonly ILogger<IGetBookStockByBookId> _logger;
        private readonly IBookStockRepository _bookStockRepository;
        public GetBookStockByBookId(ILogger<IGetBookStockByBookId> logger, IBookStockRepository bookStockRepository)
        {
            _bookStockRepository = bookStockRepository ?? throw new ArgumentNullException(nameof(bookStockRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetBookStockByBookIdResponseDTO> Handle(Guid bookId)
        {
            var getBookStockByBookIdResponse = new GetBookStockByBookIdResponseDTO();
            if(bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            var bookStock = await _bookStockRepository.GetByGuidAsync(bookId);
            if(bookStock?.BookStockId == Guid.Empty)
            {
                throw new ApplicationException($"Cannot find book stock with book id {bookId}");
            }

            getBookStockByBookIdResponse.BookId = bookStock.BookId;
            getBookStockByBookIdResponse.Stock = bookStock.Quantity;

            return getBookStockByBookIdResponse;
        }
    }
}
