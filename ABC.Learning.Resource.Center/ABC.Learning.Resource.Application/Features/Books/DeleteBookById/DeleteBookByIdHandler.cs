using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Exceptions;
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

        public async Task<bool> Handle(DeleteBookByIdRequestDTO deleteBookByIdRequestDTO)
        {
            var updateBookValidator = new DeleteBookByIdValidation();
            var validationResult = await updateBookValidator.ValidateAsync(deleteBookByIdRequestDTO);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Delete book validation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var book = await _bookRepository.GetByGuidAsync(deleteBookByIdRequestDTO.BookId);
            if (book == null)
            {
                _logger.LogError($"Book with ID {deleteBookByIdRequestDTO.BookId} not found for deletion.");
                return false;
            }

            _logger.LogInformation($"Deleting book: {book.Title} by {book.Author} with ID: {book.BookId}");
            book.IsActive = false; 
            book.BookId = deleteBookByIdRequestDTO.BookId;
            book.LastModifiedBy = deleteBookByIdRequestDTO.ModifiedBy;
            await _bookRepository.UpdateAsync(book);
            _logger.LogInformation($"Book {book.Title} deleted successfully with ID: {book.BookId}");

            return true;
        }
    }
}
