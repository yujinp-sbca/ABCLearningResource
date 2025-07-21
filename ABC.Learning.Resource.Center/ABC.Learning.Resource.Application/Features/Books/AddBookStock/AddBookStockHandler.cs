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
    public class AddBookStockHandler : IAddBookStockHandler
    {
        private readonly IBookStockRepository _bookStockRepository;
        private readonly ILogger<IAddBookStockHandler> _logger;
        public AddBookStockHandler(IBookStockRepository bookStockRepository, ILogger<IAddBookStockHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookStockRepository = bookStockRepository ?? throw new ArgumentNullException(nameof(bookStockRepository));
        }

        public async Task<AddBookStockResponseDTO> Handle(AddBookStockRequestDTO addBookStockRequestDTO)
        {
            if(addBookStockRequestDTO == null || addBookStockRequestDTO?.Stock <= 0)
            {
                throw new ApplicationException("Invalid parameters for book stock.");
            }

            var newBookStock = new BookStock()
            {
                BookStockId = Guid.NewGuid(),
                BookId = addBookStockRequestDTO.BookId,
                Quantity = addBookStockRequestDTO.Stock,
                CreatedBy = addBookStockRequestDTO.CreatedBy,
                CreatedDate = DateTime.Now,
                LastModifiedBy = addBookStockRequestDTO.CreatedBy,
                LastModifiedDate = DateTime.Now
            };

            _logger.LogInformation("Adding new book stock");
            var newBookStockResponse = await _bookStockRepository.AddAsync(newBookStock);
            _logger.LogInformation("Successfully added new book stock");

            return new AddBookStockResponseDTO()
            {
                BookId = newBookStock.BookId,
                Stock = newBookStock.Quantity
            };
        }
    }
}
