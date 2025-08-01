using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class AddBookRequestHandler : IAddBookRequestHandler
    {
        private readonly IBookRequestTransactionRepository _bookTransactionRepository;
        private readonly ILogger<IAddBookRequestHandler> _logger;
        public AddBookRequestHandler(ILogger<IAddBookRequestHandler> logger, IBookRequestTransactionRepository bookTransactionRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookTransactionRepository = bookTransactionRepository ?? throw new ArgumentNullException(nameof(bookTransactionRepository));
        }

        public async Task<AddBookRequestResponseDTO> Handle(AddBookRequestDTO addBookRequest)
        {
            var addBookReservatioValidator = new AddBookRequestValidation();
            var validationResult = await addBookReservatioValidator.ValidateAsync(addBookRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Add book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var modifiedBy = addBookRequest.IsReservation ? addBookRequest.RequestedBy : addBookRequest.ModifiedBy;

            var bookTransaction = new BookRequestTransaction() { 
                TransactionId = Guid.NewGuid(),
                BookId = addBookRequest.BookId,                 
                IsActive = true,
                BorrowedBy = addBookRequest.RequestedBy,
                CreatedBy = modifiedBy,                   
                LastModifiedBy = modifiedBy
            };

            _logger.LogInformation("Adding book request.");
            var response = await _bookTransactionRepository.AddAsync(bookTransaction);            
            if (response?.TransactionId == Guid.Empty)
            {
                _logger.LogError($"Failed to add book request for BookId {addBookRequest.BookId}.");
                throw new ApplicationException($"Failed to add book request for BookId {addBookRequest.BookId}.");
            }

            _logger.LogInformation("Successfully reserved book.");

            return new AddBookRequestResponseDTO()
            {
                TransactionId = response.TransactionId,
                BookId = response.BookId
            };
        }
    }
}
