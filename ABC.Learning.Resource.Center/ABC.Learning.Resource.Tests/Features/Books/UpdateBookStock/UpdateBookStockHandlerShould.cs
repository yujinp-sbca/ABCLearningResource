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
    public class UpdateBookStockHandlerShould
    {
        private readonly Mock<ILogger<IUpdateBookStockHandler>> _mockLogger;
        private readonly Mock<IBookStockRepository> _mockBookStockRepository;
        public UpdateBookStockHandlerShould()
        {
            _mockLogger = new Mock<ILogger<IUpdateBookStockHandler>>();
            _mockBookStockRepository = new Mock<IBookStockRepository>();
        }

        [Fact]
        public void ThrowError_WhenUpdateBookStockRequestIsInvalid_OnUpdateBookStockHandle()
        {
            // Arrange
            var updateBookStockHandler = new UpdateBookStockHandler(_mockLogger.Object, _mockBookStockRepository.Object);
            var updateBookStockRequestDTO = new UpdateBookStockRequestDTO();
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookStockHandler.Handle(updateBookStockRequestDTO));
        }

        [Fact]
        public async Task UpdateBookStock_WhenPassingValidUpdateBookStockRequest_OnUpdateBookStockHandle()
        {
            // Arrange
            var updateBookStockHandler = new UpdateBookStockHandler(_mockLogger.Object, _mockBookStockRepository.Object);
            var updateBookStockRequestDTO = new UpdateBookStockRequestDTO()
            {
                BookStockId = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                BookStock = 10,
                ModifiedBy = Guid.NewGuid()
            };
            
            var updatedBookStock = new BookStock()
            {
                BookStockId = updateBookStockRequestDTO.BookStockId,
                BookId = updateBookStockRequestDTO.BookId,
                Quantity = 10,
                LastModifiedBy = Guid.NewGuid()
            };
            
            _mockBookStockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BookStock>()))
                .ReturnsAsync(updatedBookStock);

            // Act
            var result = await updateBookStockHandler.Handle(updateBookStockRequestDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateBookStockRequestDTO.BookStockId, result.BookStockId);
            Assert.Equal(updateBookStockRequestDTO.BookId, result.BookId);
            Assert.Equal(updateBookStockRequestDTO.BookStock, result.BookStock);
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenUpdatingBookStock_OnUpdateBookStockHandle()
        {
            // Arrange
            var updateBookStockHandler = new UpdateBookStockHandler(_mockLogger.Object, _mockBookStockRepository.Object);
            var updateBookStockRequestDTO = new UpdateBookStockRequestDTO()
            {
                BookId = Guid.NewGuid(),
                BookStock = 10,
                ModifiedBy = Guid.NewGuid()
            };

            var updateBookStock = new BookStock()
            {
                BookStockId = updateBookStockRequestDTO.BookId,
                BookId = updateBookStockRequestDTO.BookId,
                Quantity = updateBookStockRequestDTO.BookStock,                
                LastModifiedBy = updateBookStockRequestDTO.ModifiedBy
                
            };

            _mockBookStockRepository.Setup(repo => repo.GetByBookIdAsync(updateBookStockRequestDTO.BookId))
                .ReturnsAsync(updateBookStock); // Simulate not found
            
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookStockHandler.Handle(updateBookStockRequestDTO));
        }
    }
}
