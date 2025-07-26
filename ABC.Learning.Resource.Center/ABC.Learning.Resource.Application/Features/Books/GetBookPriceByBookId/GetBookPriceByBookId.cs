using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class GetBookPriceByBookId : IGetBookPriceByBookId
    {
        private readonly IBookPriceRepository _bookPriceRepository;                                                                                                           
        private readonly ILogger<IGetBookPriceByBookId> _logger;
        public GetBookPriceByBookId(IBookPriceRepository bookPriceRepository, ILogger<IGetBookPriceByBookId> logger)
        {
            _bookPriceRepository = bookPriceRepository ?? throw new ArgumentNullException(nameof(bookPriceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetBookPriceByBookIdResponseDTO> Handle(Guid bookId)
        {
            var getBookPriceByBookIdResponseDTO = new GetBookPriceByBookIdResponseDTO();

            if(bookId == Guid.Empty)
            {
                throw new ApplicationException("Book id is empty");
            }

            var bookPrice = await _bookPriceRepository.GetByGuidAsync(bookId);
            if (bookPrice == null) {
                throw new ApplicationException($"Unable to find book with book id {bookId}");
            }

            getBookPriceByBookIdResponseDTO.BookId = bookPrice.BookId;
            getBookPriceByBookIdResponseDTO.Price = bookPrice.Price;

            return getBookPriceByBookIdResponseDTO;
        }
    }
}
