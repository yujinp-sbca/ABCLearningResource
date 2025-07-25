using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookStockHandler : IUpdateBookStockHandler
    {
        private readonly ILogger<IUpdateBookStockHandler> _logger;
        private readonly IBookStockRepository _bookStockRepository;

        public UpdateBookStockHandler(ILogger<IUpdateBookStockHandler> logger, IBookStockRepository bookStockRepository)
        {
            _bookStockRepository = bookStockRepository ?? throw new ArgumentNullException(nameof(bookStockRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UpdateBookStockResponseDTO> Handle(UpdateBookStockRequestDTO updateBookStockDTO)
        {
            var updateBookStockValidator = new UpdateBookStockValidation();
            var validationResult = await updateBookStockValidator.ValidateAsync(updateBookStockDTO);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Update book stock validation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var currentBookStock = await _bookStockRepository.GetByBookIdAsync(updateBookStockDTO.BookId);
            if (currentBookStock == null)
            {
                _logger.LogError($"Book stock for BookId {updateBookStockDTO.BookId} not found.");
                throw new ApplicationException($"Book stock for BookId {updateBookStockDTO.BookId} not found.");
            }

            currentBookStock.Quantity = updateBookStockDTO.BookStock;
            currentBookStock.LastModifiedBy = updateBookStockDTO.ModifiedBy;

            var updateBookStockResponseDTO = await _bookStockRepository.UpdateAsync(currentBookStock);
            if(updateBookStockResponseDTO.BookId == Guid.Empty)
            {
                _logger.LogError($"Failed to update book stock for BookId {updateBookStockDTO.BookId}.");
                throw new ApplicationException($"Failed to update book stock for BookId {updateBookStockDTO.BookId}.");
            }

            return new UpdateBookStockResponseDTO()
            {
                BookId = updateBookStockResponseDTO.BookId,
                BookStock = updateBookStockResponseDTO.Quantity
            };
        }
    }
}
