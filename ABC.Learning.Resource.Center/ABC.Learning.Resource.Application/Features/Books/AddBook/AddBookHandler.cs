using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookHandler : IAddBookHandler
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<IAddBookHandler> _logger;
        public AddBookHandler(IBookRepository bookRepository, ILogger<IAddBookHandler> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<AddBookResponseDTO> Handle(AddBookRequestDTO addBookDTO)
        {
            var addBookValidator = new AddBookValidation();
            var validationResult = await addBookValidator.ValidateAsync(addBookDTO);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Add book validation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }
                                                      
            var newBook = new Book
            {
                BookId = Guid.NewGuid(),
                Title = addBookDTO.Title,
                Author = addBookDTO.Author,
                CategoryId = addBookDTO.CategoryId,
                Abstract = addBookDTO.Abstract,
                ISBN = addBookDTO.ISBN,
                PublishedDate = addBookDTO.PublishedDate,
                Publisher = addBookDTO.Publisher,
                Language = addBookDTO.Language,
                Description = addBookDTO.Description,
                CoverImageUrl = addBookDTO.CoverImageUrl,
                CreatedBy = addBookDTO.CreatedBy,                
                LastModifiedBy = addBookDTO.CreatedBy,                
                IsActive = true
            };

            _logger.LogInformation($"Adding book: {newBook.Title} by {newBook.Author}");
            var newBookResponse = await _bookRepository.AddAsync(newBook);
            _logger.LogInformation($"Book {newBook.Title} added successfully with ID: {newBook.BookId}");

            return new AddBookResponseDTO()
            {
                BookId = newBookResponse.BookId,
                Title = newBookResponse.Title,
                Author = newBookResponse.Author,
                ISBN = newBookResponse.ISBN,
                Abstract = newBookResponse.Abstract
            };
        }
    }
}
