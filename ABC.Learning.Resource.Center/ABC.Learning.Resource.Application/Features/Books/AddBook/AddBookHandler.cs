using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
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
            if (addBookDTO == null)
            {                
                throw new ApplicationException("Invalid book parameter.");
            }
                                                      
            var book = new Book
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
                CreatedBy = "Administrator",
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = "Administrator",
                LastModifiedDate = DateTime.UtcNow
            };

            _logger.LogInformation($"Adding book: {book.Title} by {book.Author}");
            await _bookRepository.AddAsync(book);
            _logger.LogInformation($"Book {book.Title} added successfully with ID: {book.BookId}");

            return new AddBookResponseDTO()
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Abstract = book.Abstract
            };
        }
    }
}
