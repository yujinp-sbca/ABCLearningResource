using ABC.Learning.Resource.Application.Features.Books;
using ABC.Learning.Resource.Application.Features.User;
using ABC.Learning.Resource.Application.Services.Book.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Services.Book
{
    public class BookService : IBookService
    {
        private readonly IAddBookHandler _addBookHandler;
        private readonly IAddBookStockHandler _addBookStockHandler;
        private readonly IAddBookPriceHandler _addBookPriceHandler;
        private readonly IUpdateBookHandler _updateBookHandler;
        private readonly IUpdateBookPriceHandler _updateBookPriceHandler;
        private readonly IUpdateBookStockHandler _updateBookStockHandler;
        private readonly IDeleteBookByIdHandler _deleteBookByIdHandler;
        private readonly IGetBookByIdHandler _getBookByIdHandler;
        private readonly IGetActiveUserByUserIdHandler _getActiveUserByEmailHandler;
        private readonly ILogger<IBookService> _logger;
        public BookService(IAddBookHandler addBookHandler, IAddBookPriceHandler addBookPriceHandler, IAddBookStockHandler addBookStockHandler, IUpdateBookHandler updateBookHandler, IUpdateBookPriceHandler updateBookPriceHandler, IUpdateBookStockHandler updateBookStockHandler, IDeleteBookByIdHandler deleteBookByIdHandler, IGetBookByIdHandler getBookByIdHandler, IGetActiveUserByUserIdHandler getActiveUserByEmailHandler, ILogger<IBookService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _addBookHandler = addBookHandler ?? throw new ArgumentNullException(nameof(addBookHandler));
            _addBookPriceHandler = addBookPriceHandler ?? throw new ArgumentNullException(nameof(addBookPriceHandler));
            _addBookStockHandler = addBookStockHandler ?? throw new ArgumentNullException(nameof(addBookStockHandler));
            _updateBookHandler = updateBookHandler ?? throw new ArgumentNullException(nameof(updateBookHandler));
            _updateBookPriceHandler = updateBookPriceHandler ?? throw new ArgumentNullException(nameof(updateBookPriceHandler));
            _updateBookStockHandler = updateBookStockHandler ?? throw new ArgumentNullException(nameof(updateBookStockHandler));
            _deleteBookByIdHandler = deleteBookByIdHandler ?? throw new ArgumentNullException(nameof(deleteBookByIdHandler));
            _getBookByIdHandler = getBookByIdHandler ?? throw new ArgumentNullException(nameof(getBookByIdHandler));
            _getActiveUserByEmailHandler = getActiveUserByEmailHandler ?? throw new ArgumentNullException(nameof(getActiveUserByEmailHandler));
        }

        public async Task<AddBookServiceResponseDTO> AddBook(AddBookServiceRequestDTO bookServiceRequestDTO)
        {
            var newBookServiceResponseDTO = new AddBookServiceResponseDTO();

            var user = await _getActiveUserByEmailHandler.Handle(bookServiceRequestDTO.CreatedBy);
            if(user == null)
            {
                throw new ApplicationException($"User with email {bookServiceRequestDTO.CreatedBy} does not exist or is not active.");
            }

            if(!user.IsAdmin)
            {
                _logger.LogError($"User with email {bookServiceRequestDTO.CreatedBy} is not an admin and cannot add books.");
                throw new UnauthorizedAccessException($"User with email {bookServiceRequestDTO.CreatedBy} is not authorized to add books.");
            }

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

            if (newBookResponseDTO?.BookId == Guid.Empty)
            {
                throw new ApplicationException("Failed to add new book.");
            }

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

            return newBookServiceResponseDTO;
        }        

        public async Task<UpdateBookServiceResponseDTO> UpdateBook(UpdateBookServiceRequestDTO bookServiceRequestDTO)
        {
            var updateBookServiceResponseDTO = new UpdateBookServiceResponseDTO();

            var user = await _getActiveUserByEmailHandler.Handle(bookServiceRequestDTO.ModifiedBy);
            if (user == null)
            {
                throw new ApplicationException($"User with email {bookServiceRequestDTO.ModifiedBy} does not exist or is not active.");
            }

            if (!user.IsAdmin)
            {
                _logger.LogError($"User with email {bookServiceRequestDTO.ModifiedBy} is not an admin and cannot update books.");
                throw new UnauthorizedAccessException($"User with email {bookServiceRequestDTO.ModifiedBy} is not authorized to update books.");
            }

            var currentBook = await _getBookByIdHandler.Handle(bookServiceRequestDTO.BookId);

            if(currentBook?.BookId == Guid.Empty)
            {
                throw new ApplicationException($"Book with ID {bookServiceRequestDTO.BookId} does not exist.");
            }

            var updateBookRequestDTO = new UpdateBookRequestDTO
            {
                Title = currentBook.Title,
                Author = currentBook.Author,
                CategoryId = currentBook.CategoryId,
                Abstract = currentBook.Abstract,
                ISBN = currentBook.ISBN,
                PublishedDate = currentBook.PublishedDate,
                Publisher = currentBook.Publisher,
                Language = currentBook.Language,
                Description = currentBook.Description,
                CoverImageUrl = currentBook.CoverImageUrl,
                ModifiedBy = bookServiceRequestDTO.ModifiedBy
            };

            var updateBookResponseDTO = await _updateBookHandler.Handle(updateBookRequestDTO);

            if (updateBookResponseDTO?.BookId == Guid.Empty)
            {
                throw new ApplicationException("Failed to add new book.");
            }

            var updateBookPriceResponseDTO = await _updateBookPriceHandler.Handle(
                        new UpdateBookPriceRequestDTO()
                        {
                            BookId = bookServiceRequestDTO.BookId,
                            Price = bookServiceRequestDTO.Price,
                            ModifiedBy = bookServiceRequestDTO.ModifiedBy
                        }
                    );

            var updateBookStockResponseDTO = await _updateBookStockHandler.Handle(                                                                        
                        new UpdateBookStockRequestDTO()
                        {
                            BookId = bookServiceRequestDTO.BookId,
                            BookStock = bookServiceRequestDTO.Stock,
                            ModifiedBy  = bookServiceRequestDTO.ModifiedBy
                        }
                    );

            updateBookServiceResponseDTO.BookId = updateBookResponseDTO.BookId;
            updateBookServiceResponseDTO.Title = updateBookResponseDTO.Title;
            updateBookServiceResponseDTO.Author = updateBookResponseDTO.Author;
            updateBookServiceResponseDTO.ISBN = updateBookResponseDTO.ISBN;
            updateBookServiceResponseDTO.Abstract = updateBookResponseDTO.Abstract;
            updateBookServiceResponseDTO.Stock = updateBookStockResponseDTO.BookStock;
            updateBookServiceResponseDTO.Price = updateBookPriceResponseDTO.Price;

            return updateBookServiceResponseDTO;
        }

        public async Task<bool> DeleteBook(DeleteBookServiceRequestDTO deleteBookServiceRequestDTO)
        {
            var user = await _getActiveUserByEmailHandler.Handle(deleteBookServiceRequestDTO.ModifiedBy);
            if (user == null)
            {
                throw new ApplicationException($"User with email {deleteBookServiceRequestDTO.ModifiedBy} does not exist or is not active.");
            }

            if (!user.IsAdmin)
            {
                _logger.LogError($"User with email {deleteBookServiceRequestDTO.ModifiedBy} is not an admin and cannot delete books.");
                throw new UnauthorizedAccessException($"User with email {deleteBookServiceRequestDTO.ModifiedBy} is not authorized to delete books.");
            }

            var deleteBookByIdRequestDTO = new DeleteBookByIdRequestDTO()
            {
                BookId = deleteBookServiceRequestDTO.BookId,
                ModifiedBy = deleteBookServiceRequestDTO.ModifiedBy
            };

            return await _deleteBookByIdHandler.Handle(deleteBookByIdRequestDTO);
        }
    }
}
