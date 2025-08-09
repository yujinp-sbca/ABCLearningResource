using ABC.Learning.Resource.Application.Contracts.Persistence;
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
    public class AddBookPriceHandlerShould
    {
        private readonly Mock<IBookPriceRepository> _mockBookPriceRepository;
        private readonly Mock<ILogger<IAddBookPriceHandler>> _mockLogger;

        public AddBookPriceHandlerShould()
        {
            _mockBookPriceRepository = new Mock<IBookPriceRepository>();
            _mockLogger = new Mock<ILogger<IAddBookPriceHandler>>();
        }

        [Fact]
        public void ThrowError_WhenAddBookPriceRequestIsInvalid_OnAddBookPriceHandle()
        {
            // Arrange
            var addBookPriceHandler = new AddBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var addBookPriceRequestDTO = new AddBookPriceRequestDTO();
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await addBookPriceHandler.Handle(addBookPriceRequestDTO));
        }

        [Fact]
        public async Task AddBookPrice_WhenPassingValidAddBookPriceRequest_OnAddBookPriceHandle()
        {
            // Arrange
            var addBookPriceHandler = new AddBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var addBookPriceRequestDTO = new AddBookPriceRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Price = 19.99,
                CreatedBy = Guid.NewGuid()
            };
            var newBookPrice = new BookPrice()
            {
                BookPriceId = Guid.NewGuid(),
                BookId = addBookPriceRequestDTO.BookId,
                Price = addBookPriceRequestDTO.Price,
                IsActive = true,
                LastModifiedBy = addBookPriceRequestDTO.CreatedBy,
                CreatedBy = addBookPriceRequestDTO.CreatedBy
            };
            _mockBookPriceRepository.Setup(repo => repo.AddAsync(It.IsAny<BookPrice>()))
                                    .ReturnsAsync(newBookPrice);
            // Act
            var result = await addBookPriceHandler.Handle(addBookPriceRequestDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(addBookPriceRequestDTO.BookId, result.BookId);
            Assert.Equal(addBookPriceRequestDTO.Price, result.Price);
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenAddingBookPrice_OnAddBookPriceHandle()
        {
            // Arrange
            var addBookPriceHandler = new AddBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var addBookPriceRequestDTO = new AddBookPriceRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Price = 19.99,
                CreatedBy = Guid.NewGuid()
            };
            _mockBookPriceRepository.Setup(repo => repo.AddAsync(It.IsAny<BookPrice>()))
                                    .Throws(new ApplicationException("Error adding book price."));
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await addBookPriceHandler.Handle(addBookPriceRequestDTO));
        }
    }
}
