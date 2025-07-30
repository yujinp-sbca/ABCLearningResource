using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public class AddBookReservationHandler : IAddBookReservationHandler
    {
        private readonly IBookReservationTransactionRepository _bookTransactionRepository;
        private readonly ILogger<IAddBookReservationHandler> _logger;
        public AddBookReservationHandler(ILogger<IAddBookReservationHandler> logger, IBookReservationTransactionRepository bookTransactionRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookTransactionRepository = bookTransactionRepository ?? throw new ArgumentNullException(nameof(bookTransactionRepository));
        }

        public async Task<AddBookReservationResponseDTO> Handle(AddBookReservationRequestDTO addBookReservationRequest)
        {
            var addBookReservatioValidator = new AddBookReservationValidation();
            var validationResult = await addBookReservatioValidator.ValidateAsync(addBookReservationRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Add book reservation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookTransaction = new BookReservationTransaction() { 
                TransactionId = Guid.NewGuid(),
                BookId = addBookReservationRequest.BookId,                 
                IsActive = true,
                IsBorrowed = false,
                CreatedBy = addBookReservationRequest.UserId,                
                LastModifiedBy = addBookReservationRequest.UserId
            };

            _logger.LogInformation("Adding book reservation.");
            var response = await _bookTransactionRepository.AddAsync(bookTransaction);
            _logger.LogInformation("Successfully reserved book.");


            if (response?.TransactionId == Guid.Empty)
            {
                _logger.LogError($"Failed to add book reservation for BookId {addBookReservationRequest.BookId}.");
                throw new ApplicationException($"Failed to add book reservation for BookId {addBookReservationRequest.BookId}.");
            }

            return new AddBookReservationResponseDTO()
            {
                TransactionId = response.TransactionId,
                BookId = response.BookId
            };
        }
    }
}
