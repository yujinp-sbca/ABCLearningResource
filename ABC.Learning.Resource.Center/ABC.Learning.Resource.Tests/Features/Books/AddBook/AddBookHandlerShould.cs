using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Application.Features.Books;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System.Runtime.CompilerServices;

namespace ABC.Learning.Resource.Application.Tests.Features.Books
{
    public class AddBookHandlerShould
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<ILogger<IAddBookHandler>> _mockLogger;
        public AddBookHandlerShould()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockLogger = new Mock<ILogger<IAddBookHandler>>();
        }

        [Fact]       
        public void ThrowError_WhenAddBookRequestIsInvalid_OnAddBookHandle()
        {
            //Arrange
            var addBookHandler = new AddBookHandler(_mockBookRepository.Object, _mockLogger.Object);
            var addbookRequestDTO = new AddBookRequestDTO();

            //Act and Assert
            Assert.ThrowsAsync<ValidationException>(async () => await addBookHandler.Handle(addbookRequestDTO));
            //Assert
        }

        [Fact]
        public async Task AddNewBook_WhenPassingValidAddBookRequest_OnAddBookHandle()
        {
            //Arrange
            var addBookHandler = new AddBookHandler(_mockBookRepository.Object, _mockLogger.Object);
            var addbookRequestDTO = new AddBookRequestDTO()
            {
                Title = "addBookDTO.Title",
                Author = "addBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "addBookDTO.Abstract",
                ISBN = "addBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "addBookDTO.Publisher",
                Language = "addBookDTO.Language",
                Description = "addBookDTO.Description",
                CoverImageUrl = "addBookDTO.CoverImageUrl",
                CreatedBy = Guid.NewGuid()
            };

            var newBook = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "addBookDTO.Title",
                Author = "addBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "addBookDTO.Abstract",
                ISBN = "addBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "addBookDTO.Publisher",
                Language = "addBookDTO.Language",
                Description = "addBookDTO.Description",
                CoverImageUrl = "addBookDTO.CoverImageUrl",
                CreatedBy = addbookRequestDTO.CreatedBy,
                LastModifiedBy = addbookRequestDTO.CreatedBy,
                IsActive = true
            };

            _mockBookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .ReturnsAsync(newBook);

            var response = await addBookHandler.Handle(addbookRequestDTO);

            //Assert
            Assert.Equal(newBook.BookId, response.BookId);
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenAddingBook_OnAddBookHandle()
        {
            //Arrange
            var addBookHandler = new AddBookHandler(_mockBookRepository.Object, _mockLogger.Object);
            var addbookRequestDTO = new AddBookRequestDTO()
            {
                Title = "addBookDTO.Title",
                Author = "addBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "addBookDTO.Abstract",
                ISBN = "addBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "addBookDTO.Publisher",
                Language = "addBookDTO.Language",
                Description = "addBookDTO.Description",
                CoverImageUrl = "addBookDTO.CoverImageUrl",
                CreatedBy = Guid.NewGuid()
            };

            var newBook = new Book()
            {
                BookId = Guid.Empty,
                Title = "addBookDTO.Title",
                Author = "addBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "addBookDTO.Abstract",
                ISBN = "addBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "addBookDTO.Publisher",
                Language = "addBookDTO.Language",
                Description = "addBookDTO.Description",
                CoverImageUrl = "addBookDTO.CoverImageUrl",
                CreatedBy = addbookRequestDTO.CreatedBy,
                LastModifiedBy = addbookRequestDTO.CreatedBy,
                IsActive = true
            };

            _mockBookRepository
                .Setup(r => r.AddAsync(It.IsAny<Book>()))
                .ReturnsAsync(newBook);

            //Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await addBookHandler.Handle(addbookRequestDTO));
        }
    }
}