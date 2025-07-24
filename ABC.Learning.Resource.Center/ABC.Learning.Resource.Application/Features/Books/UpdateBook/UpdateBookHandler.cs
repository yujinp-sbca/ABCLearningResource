using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookHandler : IUpdateBookHandler
    {
        private readonly ILogger<IUpdateBookHandler> _logger;
        private readonly IBookRepository _bookRepository;

        public UpdateBookHandler(ILogger<IUpdateBookHandler> logger, IBookRepository bookRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<UpdateBookResponseDTO> Handle(UpdateBookRequestDTO updateBookRequest)
        {
            var updateBookValidation = new UpdateBookValidation();
            var validationResult = await updateBookValidation.ValidateAsync(updateBookRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Create PII Item Request Command validation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var updateBook = new Book
            {
                BookId = updateBookRequest.BookId,
                Title = updateBookRequest.Title,
                Author = updateBookRequest.Author,
                CategoryId = updateBookRequest.CategoryId,
                Abstract = updateBookRequest.Abstract,
                ISBN = updateBookRequest.ISBN,
                PublishedDate = updateBookRequest.PublishedDate,
                Publisher = updateBookRequest.Publisher,
                Language = updateBookRequest.Language,
                Description = updateBookRequest.Description,
                CoverImageUrl = updateBookRequest.CoverImageUrl,                
                LastModifiedBy = updateBookRequest.ModifiedBy,
                IsActive = true
            };

            _logger.LogInformation($"Updating book: {updateBook.Title} by {updateBook.Author}");
            var updateBookResponse = await _bookRepository.UpdateAsync(updateBook);
            _logger.LogInformation($"Book {updateBook.Title} added successfully with ID: {updateBook.BookId}");

            return new UpdateBookResponseDTO()
            {
                BookId = updateBookResponse.BookId,
                Title = updateBookResponse.Title,
                Author = updateBookResponse.Author,
                ISBN = updateBookResponse.ISBN,
                Abstract = updateBookResponse.Abstract
            };
        }
    }
}
