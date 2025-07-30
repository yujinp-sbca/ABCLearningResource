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
    public class CancelBookReservation : ICancelBookReservation
    {
        private readonly IBookReservationTransactionRepository _bookReservationTransactionRepository;
        private readonly ILogger<ICancelBookReservation> _logger;
        public CancelBookReservation(IBookReservationTransactionRepository bookReservationTransactionRepository, ILogger<ICancelBookReservation> logger)
        {
            _bookReservationTransactionRepository = bookReservationTransactionRepository ?? throw new ArgumentNullException(nameof(bookReservationTransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CancelBookReservationRequestDTO cancelBookReservationRequest)
        {
            var cancelBookReservatioValidator = new CancelBookReservationValidation();
            var validationResult = await cancelBookReservatioValidator.ValidateAsync(cancelBookReservationRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Cancel book reservation failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookReservationTransaction = new BookReservationTransaction()
            {
                TransactionId = cancelBookReservationRequest.TransactionId,
                IsActive = false,
                IsBorrowed = false,
                LastModifiedBy = cancelBookReservationRequest.UserId
            };

            _logger.LogInformation("Cancelling book reservation.");
            var response = await _bookReservationTransactionRepository.UpdateAsync(bookReservationTransaction);

            if (response?.TransactionId == null)
            {
                _logger.LogError("Error cancelling book reservation");
                throw new ApplicationException("Error cancelling book reservation");
            }

            _logger.LogInformation("Successfully cancelled book reservation.");            

            return true;
        }
    }
}
