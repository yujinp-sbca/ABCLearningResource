using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class BorrowBookHandler : IBorrowBookHandler
    {
        private readonly IBookRequestTransactionRepository _bookTransactionRepository;
        private readonly ILogger<IBorrowBookHandler> _logger;
        public BorrowBookHandler(IBookRequestTransactionRepository bookTransactionRepository, ILogger<IBorrowBookHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookTransactionRepository = bookTransactionRepository ?? throw new ArgumentNullException(nameof(bookTransactionRepository));
        }

        public async Task<bool> Handle(BorrowBookRequestDTO borrowBookReservationRequest)
        {
            var borrowBookReservatioValidator = new BorrowBookValidation();
            var validationResult = await borrowBookReservatioValidator.ValidateAsync(borrowBookReservationRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Borrow book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var borrowBookReservation = new BookRequestTransaction()
            {
                TransactionId = borrowBookReservationRequest.TransactionId,
                BookId = borrowBookReservationRequest.BookId,
                IsActive = false,                
                LastModifiedBy = borrowBookReservationRequest.BorrowedBy
            };

            _logger.LogInformation("Updating book request to mark as borrowed.");
            var response = await _bookTransactionRepository.UpdateAsync(borrowBookReservation);
            _logger.LogInformation("Successfully updated book request to mark as borrowed.");

            if(response?.TransactionId == Guid.Empty)
            {
                _logger.LogError($"Failed to update book request for TransactionId {borrowBookReservationRequest.TransactionId}.");
                throw new ApplicationException($"Failed to update book request for TransactionId {borrowBookReservationRequest.TransactionId}.");
            }

            return true;
        }
    }
}
