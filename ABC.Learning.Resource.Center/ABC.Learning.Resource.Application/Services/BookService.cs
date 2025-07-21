
using ABC.Learning.Resource.Application.Features.Books;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IAddBookHandler _addBookHandler;
        private readonly IAddBookStockHandler _addBookStockHandler;
        private readonly IAddBookPriceHandler _addBookPriceHandler;
        private readonly ILogger<IBookService> _logger;
        public BookService(IAddBookHandler addBookHandler, IAddBookPriceHandler addBookPriceHandler, IAddBookStockHandler addBookStockHandler, ILogger<IBookService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _addBookHandler = addBookHandler ?? throw new ArgumentNullException(nameof(addBookHandler));
            _addBookPriceHandler = addBookPriceHandler ?? throw new ArgumentNullException(nameof(addBookPriceHandler));
            _addBookStockHandler = addBookStockHandler ?? throw new ArgumentNullException(nameof(addBookStockHandler));
        }

        public async Task<AddBookServiceResponseDTO> AddBook(AddBookServiceRequestDTO bookServiceRequestDTO)
        {
            var newBookServiceResponseDTO = new AddBookServiceResponseDTO();

            var newBookRequestDTO = new AddBookRequestDTO
            {
                Title = bookServiceRequestDTO.Title,
                Author = bookServiceRequestDTO.Author,
                CategoryId = bookServiceRequestDTO.CategoryId,
                Abstract = bookServiceRequestDTO.Abstract,
                ISBN = bookServiceRequestDTO.ISBN,
                PublishedDate = bookServiceRequestDTO.PublishedDate,
                Publisher = bookServiceRequestDTO.Publisher,
                Language = bookServiceRequestDTO.Language,
                Description = bookServiceRequestDTO.Description,
                CoverImageUrl = bookServiceRequestDTO.CoverImageUrl,
                CreatedBy = bookServiceRequestDTO.CreatedBy
            };

            var newBookResponseDTO = await _addBookHandler.Handle(newBookRequestDTO);

            if(newBookResponseDTO?.BookId != Guid.Empty)
            {
                var newBookPriceResponseDTO = await _addBookPriceHandler.Handle(
                        new AddBookPriceRequestDTO()
                        {
                            BookId = newBookResponseDTO.BookId,
                            Price = bookServiceRequestDTO.Price,
                            CreatedBy = bookServiceRequestDTO.CreatedBy
                        }
                    );

                var newBookStockResponseDTO = await _addBookStockHandler.Handle(
                        new AddBookStockRequestDTO()
                        {
                            BookId = newBookResponseDTO.BookId,
                            Stock = bookServiceRequestDTO.Stock,
                            CreatedBy = bookServiceRequestDTO.CreatedBy                            
                        }
                    );

                newBookServiceResponseDTO.BookId = newBookResponseDTO.BookId;
                newBookServiceResponseDTO.Title = newBookResponseDTO.Title;
                newBookServiceResponseDTO.Author = newBookResponseDTO.Author;
                newBookServiceResponseDTO.ISBN = newBookResponseDTO.ISBN;
                newBookServiceResponseDTO.Abstract = newBookResponseDTO.Abstract;
                newBookServiceResponseDTO.Stock = newBookStockResponseDTO.Stock;
                newBookServiceResponseDTO.Price = newBookPriceResponseDTO.Price;
            }

            return newBookServiceResponseDTO;
        }
    }
}
