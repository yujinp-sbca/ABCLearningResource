using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Application.Features.BookRequest;
using ABC.Learning.Resource.Application.Features.Books;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Tests.Features.Books
{
    public class UpdateBookHandlerShould
    {
        private readonly Mock<ILogger<IUpdateBookHandler>> _mockLogger;
        private readonly Mock<IBookRepository> _mockBookRepository;
        public UpdateBookHandlerShould()
        {
            _mockLogger = new Mock<ILogger<IUpdateBookHandler>>();
            _mockBookRepository = new Mock<IBookRepository>();
        }

        [Fact]
        public void ThrowError_WhenUpdateBookRequestIsInvalid_OnUpdateBookHandle()
        {
            // Arrange
            var updateBookPriceHandler = new UpdateBookHandler(_mockLogger.Object, _mockBookRepository.Object);
            var updateBookPriceRequestDTO = new UpdateBookRequestDTO();
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookPriceHandler.Handle(updateBookPriceRequestDTO));
        }

        [Fact]
        public async Task UpdateBook_WhenPassingValidUpdateBookRequest_OnUpdateBookHandle()
        {
            var updateBookHandler = new UpdateBookHandler(_mockLogger.Object, _mockBookRepository.Object);
            var updateBookRequestDTO = new UpdateBookRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Title = "updateBookDTO.Title",
                Author = "updateBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "updateBookDTO.Abstract",
                ISBN = "updateBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "updateBookDTO.Publisher",
                Language = "updateBookDTO.Language",
                Description = "updateBookDTO.Description",
                CoverImageUrl = "updateBookDTO.CoverImageUrl",
                ModifiedBy = Guid.NewGuid()
            };

            var updatedBook = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "updateBookDTO.Title",
                Author = "updateBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "updateBookDTO.Abstract",
                ISBN = "updateBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "updateBookDTO.Publisher",
                Language = "updateBookDTO.Language",
                Description = "updateBookDTO.Description",
                CoverImageUrl = "updateBookDTO.CoverImageUrl",                
                LastModifiedBy = updateBookRequestDTO.ModifiedBy,
                IsActive = true
            };

            _mockBookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(updatedBook);

            var response = await updateBookHandler.Handle(updateBookRequestDTO);

            //Assert
            Assert.Equal(updatedBook.BookId, response.BookId);
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenUpdatingBook_OnUpdateBookHandle()
        {
            //Arrange
            var updateBookHandler = new UpdateBookHandler(_mockLogger.Object, _mockBookRepository.Object);
            var updatebookRequestDTO = new UpdateBookRequestDTO()
            {
                Title = "updateBookDTO.Title",
                Author = "updateBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "updateBookDTO.Abstract",
                ISBN = "updateBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "updateBookDTO.Publisher",
                Language = "updateBookDTO.Language",
                Description = "updateBookDTO.Description",
                CoverImageUrl = "updateBookDTO.CoverImageUrl",
                ModifiedBy = Guid.NewGuid()
            };

            var updatedBook = new Book()
            {
                BookId = Guid.Empty,
                Title = "updateBookDTO.Title",
                Author = "updateBookDTO.Author",
                CategoryId = Guid.NewGuid(),
                Abstract = "updateBookDTO.Abstract",
                ISBN = "updateBookDTO.ISBN",
                PublishedDate = DateTime.Now,
                Publisher = "updateBookDTO.Publisher",
                Language = "updateBookDTO.Language",
                Description = "updateBookDTO.Description",
                CoverImageUrl = "updateBookDTO.CoverImageUrl",                
                LastModifiedBy = updatebookRequestDTO.ModifiedBy,
                IsActive = true
            };

            _mockBookRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Book>()))
                .ReturnsAsync(updatedBook);

            //Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookHandler.Handle(updatebookRequestDTO));
        }
    }
}
