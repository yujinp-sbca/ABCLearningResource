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
    public class CancelBookRequestHandler : ICancelBookRequestHandler
    {
        private readonly IBookRequestTransactionRepository _bookRequestTransactionRepository;
        private readonly ILogger<ICancelBookRequestHandler> _logger;
        public CancelBookRequestHandler(IBookRequestTransactionRepository bookRequestTransactionRepository, ILogger<ICancelBookRequestHandler> logger)
        {
            _bookRequestTransactionRepository = bookRequestTransactionRepository ?? throw new ArgumentNullException(nameof(bookRequestTransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CancelBookRequestDTO cancelBookRequestRequest)
        {
            var cancelBookRequestValidator = new CancelBookRequestValidation();
            var validationResult = await cancelBookRequestValidator.ValidateAsync(cancelBookRequestRequest);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Cancel book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookRequestTransaction = new BookRequestTransaction()
            {
                TransactionId = cancelBookRequestRequest.TransactionId,
                IsActive = false,                
                LastModifiedBy = cancelBookRequestRequest.ModifiedBy
            };

            _logger.LogInformation("Cancelling book request.");
            var response = await _bookRequestTransactionRepository.UpdateAsync(bookRequestTransaction);

            if (response?.TransactionId == null)
            {
                _logger.LogError("Error cancelling book request");
                throw new ApplicationException("Error cancelling book request");
            }

            _logger.LogInformation("Successfully cancelled book request.");            

            return true;
        }
    }
}
