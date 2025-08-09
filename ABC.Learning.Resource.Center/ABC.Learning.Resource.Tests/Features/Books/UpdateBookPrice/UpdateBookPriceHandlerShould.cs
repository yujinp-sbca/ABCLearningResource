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
    public class UpdateBookPriceHandlerShould
    {
        private readonly Mock<ILogger<IUpdateBookPriceHandler>> _mockLogger;
        private readonly Mock<IBookPriceRepository> _mockBookPriceRepository;
        public UpdateBookPriceHandlerShould()
        {
            _mockBookPriceRepository = new Mock<IBookPriceRepository>();
            _mockLogger = new Mock<ILogger<IUpdateBookPriceHandler>>();
        }

        [Fact]
        public void ThrowError_WhenUpdateBookPriceRequestIsInvalid_OnUpdateBookPriceHandle()
        {
            // Arrange
            var updateBookPriceHandler = new UpdateBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var updateBookPriceRequestDTO = new UpdateBookPriceRequestDTO();
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookPriceHandler.Handle(updateBookPriceRequestDTO));
        }

        [Fact]
        public async Task UpdateBookPrice_WhenPassingValidUpdateBookPriceRequest_OnUpdateBookPriceHandle()
        {
            // Arrange
            var updateBookPriceHandler = new UpdateBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var updateBookPriceRequestDTO = new UpdateBookPriceRequestDTO()
            {
                BookPriceId = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                Price = 19.99,
                ModifiedBy = Guid.NewGuid(),
                IsActive = false
            };

            var updatedBookPrice = new BookPrice()
            {
                BookPriceId = updateBookPriceRequestDTO.BookPriceId,
                BookId = updateBookPriceRequestDTO.BookId,
                Price = updateBookPriceRequestDTO.Price,
                IsActive = updateBookPriceRequestDTO.IsActive,
                LastModifiedBy = updateBookPriceRequestDTO.ModifiedBy
            };

            _mockBookPriceRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BookPrice>()))
                                    .ReturnsAsync(updatedBookPrice);
            // Act
            var result = await updateBookPriceHandler.Handle(updateBookPriceRequestDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBookPrice.Price, result.Price);
            Assert.Equal(updatedBookPrice.BookId, result.BookId);            
        }

        [Fact]
        public void ThrowNewApplicationException_WhenEncounteredErrorWhenUpdatingBookPrice_OnUpdateBookPriceHandle()
        {
            // Arrange
            var updateBookPriceHandler = new UpdateBookPriceHandler(_mockLogger.Object, _mockBookPriceRepository.Object);
            var updateBookPriceRequestDTO = new UpdateBookPriceRequestDTO()
            {
                BookId = Guid.NewGuid(),
                Price = 29.99,
                ModifiedBy = Guid.NewGuid()
            };

            var updatedBookPrice = new BookPrice()
            {
                BookPriceId = Guid.NewGuid(),
                BookId = updateBookPriceRequestDTO.BookId,
                Price = updateBookPriceRequestDTO.Price,
                IsActive = true,
                LastModifiedBy = updateBookPriceRequestDTO.ModifiedBy,
                CreatedBy = updateBookPriceRequestDTO.ModifiedBy
            };

            _mockBookPriceRepository.Setup(repo => repo.GetByBookIdAsync(updateBookPriceRequestDTO.BookId))
                                    .ReturnsAsync(updatedBookPrice); // Simulate not found
            // Act and Assert
            Assert.ThrowsAsync<ApplicationException>(async () => await updateBookPriceHandler.Handle(updateBookPriceRequestDTO));
        }
    }
}
