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

            var currentBookPrice = await _bookPriceRepository.GetByBookIdAsync(updateBookPriceDTO.BookId);
            if(currentBookPrice == null)
            {
                _logger.LogError($"Book price for BookId {updateBookPriceDTO.BookId} not found.");
                throw new ApplicationException($"Book price for BookId {updateBookPriceDTO.BookId} not found.");
            }

            currentBookPrice.Price = updateBookPriceDTO.Price;
            currentBookPrice.LastModifiedBy = updateBookPriceDTO.ModifiedBy;
            currentBookPrice.IsActive = false;

            var updatedBookPriceResponse = await _bookPriceRepository.UpdateAsync(currentBookPrice);

            if (updatedBookPriceResponse.BookPriceId == Guid.Empty)
            {
                _logger.LogError($"Failed to update book price for BookId {updateBookPriceDTO.BookId}.");
                throw new ApplicationException($"Failed to update book price for BookId {updateBookPriceDTO.BookId}.");
            }

            var newBookPrice = new BookPrice()
            {
                BookPriceId = new Guid(),
                BookId = updateBookPriceDTO.BookId,
                Price = updateBookPriceDTO.Price,
                IsActive = true,
                LastModifiedBy = updateBookPriceDTO.ModifiedBy,
                CreatedBy = updateBookPriceDTO.ModifiedBy
            };

            _logger.LogInformation("Adding new book price");
            var newBookPriceResponse = await _bookPriceRepository.AddAsync(newBookPrice);
            _logger.LogInformation("Successfully added new book price");

            _logger.LogInformation($"Book price for BookId {updateBookPriceDTO.BookId} updated successfully to {updateBookPriceDTO.Price}.");

            return new UpdateBookPriceResponseDTO()
            {
                BookId = newBookPriceResponse.BookId,
                Price = newBookPriceResponse.Price
            };

        }
    }
}
