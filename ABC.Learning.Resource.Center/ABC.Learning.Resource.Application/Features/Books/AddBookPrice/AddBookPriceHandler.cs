using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookPriceHandler : IAddBookPriceHandler
    {
        private readonly IBookPriceRepository _bookPriceRepository;
        private readonly ILogger<IAddBookPriceHandler> _logger;
        public AddBookPriceHandler(ILogger<IAddBookPriceHandler> logger, IBookPriceRepository bookPriceRepository)
        {            
            _bookPriceRepository = bookPriceRepository ?? throw new ArgumentNullException(nameof(bookPriceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AddBookPriceResponseDTO> Handle(AddBookPriceRequestDTO addBookPriceRequestDTO)
        {
            if(addBookPriceRequestDTO == null || addBookPriceRequestDTO?.Price <= 0)
            {                
                throw new ApplicationException("Invalid parameters for book price.");
            }

            var newBookPrice = new BookPrice()
            {
                BookPriceId = new Guid(),
                BookId = addBookPriceRequestDTO.BookId,
                Price = addBookPriceRequestDTO.Price,
                IsActive = true,
                LastModifiedBy = addBookPriceRequestDTO.CreatedBy,
                CreatedBy = addBookPriceRequestDTO.CreatedBy,
                LastModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };

            _logger.LogInformation("Adding new book price");
            var newBookPriceResponse = await _bookPriceRepository.AddAsync(newBookPrice);
            _logger.LogInformation("Successfully added new book price");

            return new AddBookPriceResponseDTO()
            {
                BookId = newBookPriceResponse.BookId,
                Price = newBookPriceResponse.Price
            };
        }
    }
}
