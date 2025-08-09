using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using ABC.Learning.Resource.Exceptions;
using ABC.Learning.Resource.Domain.Entities;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookPriceHandler : IUpdateBookPriceHandler
    {
        private readonly ILogger<IUpdateBookPriceHandler> _logger;
        private readonly IBookPriceRepository _bookPriceRepository;
        public UpdateBookPriceHandler(ILogger<IUpdateBookPriceHandler> logger, IBookPriceRepository bookPriceRepository)
        {
            _bookPriceRepository = bookPriceRepository ?? throw new ArgumentNullException(nameof(bookPriceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UpdateBookPriceResponseDTO> Handle(UpdateBookPriceRequestDTO updateBookPriceDTO)
        {
            var updateBookValidator = new UpdateBookPriceValidation();
            var validationResult = await updateBookValidator.ValidateAsync(updateBookPriceDTO);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Update book price failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var currentBookPrice = new BookPrice() {
                BookPriceId = updateBookPriceDTO.BookPriceId,
                BookId = updateBookPriceDTO.BookId,
                Price = updateBookPriceDTO.Price,
                LastModifiedBy = updateBookPriceDTO.ModifiedBy,
                IsActive = updateBookPriceDTO.IsActive
            };

            var updatedBookPriceResponse = await _bookPriceRepository.UpdateAsync(currentBookPrice);

            if (updatedBookPriceResponse.BookPriceId == Guid.Empty)
            {
                _logger.LogError($"Failed to update book price for BookId {updateBookPriceDTO.BookId}.");
                throw new ApplicationException($"Failed to update book price for BookId {updateBookPriceDTO.BookId}.");
            }           

            _logger.LogInformation($"Book price for BookId {updateBookPriceDTO.BookId} updated successfully to {updateBookPriceDTO.Price}.");

            return new UpdateBookPriceResponseDTO()
            {
                BookId = currentBookPrice.BookId,
                Price = currentBookPrice.Price
            };

        }
    }
}
