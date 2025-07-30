using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public class BorrowBookReservationHandler : IBorrowBookReservationHandler
    {
        private readonly IBookReservationTransactionRepository _bookTransactionRepository;
        private readonly ILogger<IBorrowBookReservationHandler> _logger;
        public BorrowBookReservationHandler(IBookReservationTransactionRepository bookTransactionRepository, ILogger<IBorrowBookReservationHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookTransactionRepository = bookTransactionRepository ?? throw new ArgumentNullException(nameof(bookTransactionRepository));
        }

        public async Task<bool> Handle(BorrowBookReservationRequestDTO borrowBookReservationRequest)
        {
            var borrowBookReservatioValidator = new BorrowBookReservationValidation();
            var validationResult = await borrowBookReservatioValidator.ValidateAsync(borrowBookReservationRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Borrow book reservation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var borrowBookReservation = new BookReservationTransaction()
            {
                TransactionId = borrowBookReservationRequest.TransactionId,
                BookId = borrowBookReservationRequest.BookId,
                IsActive = false,
                IsBorrowed = true, // Mark as borrowed
                LastModifiedBy = borrowBookReservationRequest.UserId
            };

            _logger.LogInformation("Updating book reservation to mark as borrowed.");
            var response = await _bookTransactionRepository.UpdateAsync(borrowBookReservation);
            _logger.LogInformation("Successfully updated book reservation to mark as borrowed.");

            if(response?.TransactionId == Guid.Empty)
            {
                _logger.LogError($"Failed to update book reservation for TransactionId {borrowBookReservationRequest.TransactionId}.");
                throw new ApplicationException($"Failed to update book reservation for TransactionId {borrowBookReservationRequest.TransactionId}.");
            }

            return true;
        }
    }
}
