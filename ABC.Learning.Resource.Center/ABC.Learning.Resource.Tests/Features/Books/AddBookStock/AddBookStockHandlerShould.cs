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
    public class AddBookStockHandlerShould
    {
        private readonly Mock<IBookStockRepository> _mockBookStockRepository;
        private readonly Mock<ILogger<IAddBookStockHandler>> _mockLogger;

        public AddBookStockHandlerShould()
        {
            _mockBookStockRepository = new Mock<IBookStockRepository>();
            _mockLogger = new Mock<ILogger<IAddBookStockHandler>>();
        }

        [Fact]
        public void ThrowError_WhenAddBookStockRequestIsInvalid_OnAddBookStockHandle()
        {
            // Arrange
            var addBookStockHandler = new AddBookStockHandler(_mockBookStockRepository.Object, _mockLogger.Object);
            var addBookStockRequestDTO = new AddBookStockRequestDTO();
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await addBookStockHandler.Handle(addBookStockRequestDTO));
        }

        [Fact]
        public async Task AddBookStock_WhenPassingValidAddBookStockRequest_OnAddBookStockHandle()
        {
            // Arrange
            var addBookStockHandler = new AddBookStockHandler(_mockBookStockRepository.Object, _mockLogger.Object);
            var addBookStockRequestDTO = new AddBookStockRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Stock = 10,
                CreatedBy = Guid.NewGuid()
            };
            var newBookStock = new BookStock()
            {
                BookStockId = Guid.NewGuid(),
                BookId = addBookStockRequestDTO.BookId,
                Quantity = addBookStockRequestDTO.Stock,
                CreatedBy = addBookStockRequestDTO.CreatedBy,
                CreatedDate = DateTime.Now,
                LastModifiedBy = addBookStockRequestDTO.CreatedBy,
                LastModifiedDate = DateTime.Now
            };
            _mockBookStockRepository.Setup(repo => repo.AddAsync(It.IsAny<BookStock>()))
                                    .ReturnsAsync(newBookStock);
            // Act
            var result = await addBookStockHandler.Handle(addBookStockRequestDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(newBookStock.BookId, result.BookId);
            Assert.Equal(newBookStock.Quantity, result.Stock);
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenAddingBookStock_OnAddBookStockHandle()
        {
            // Arrange
            var addBookStockHandler = new AddBookStockHandler(_mockBookStockRepository.Object, _mockLogger.Object);
            var addBookStockRequestDTO = new AddBookStockRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Stock = 10,
                CreatedBy = Guid.NewGuid()
            };
            _mockBookStockRepository.Setup(repo => repo.AddAsync(It.IsAny<BookStock>()))
                                    .ThrowsAsync(new ApplicationException("Error adding book stock."));
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await addBookStockHandler.Handle(addBookStockRequestDTO));
        }
    }
}
