using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Application.Features.Books.DeleteBookById;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class DeleteBookByIdHandler : IDeleteBookByIdHandler
    {
        private readonly ILogger<IDeleteBookByIdHandler> _logger;
        private readonly IBookRepository _bookRepository;
        public DeleteBookByIdHandler(ILogger<IDeleteBookByIdHandler> logger, IBookRepository bookRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<bool> Handle(Guid bookId)
        {
            if(bookId == Guid.Empty)
            {
                _logger.LogError("Invalid book ID provided for deletion.");
                return false;
            }

            var book = await _bookRepository.GetByGuidAsync(bookId);
            if (book == null)
            {
                _logger.LogError($"Book with ID {bookId} not found for deletion.");
                return false;
            }

            _logger.LogInformation($"Deleting book: {book.Title} by {book.Author} with ID: {book.BookId}");
            book.IsActive = false; 
            book.BookId = bookId;  
            await _bookRepository.UpdateAsync(book);
            _logger.LogInformation($"Book {book.Title} deleted successfully with ID: {book.BookId}");

            return true;
        }
    }
}
