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
    public class UpdateBookRequestStatusHandler : IUpdateBookRequestStatusHandler
    {
        private readonly IBookRequestTransactionRepository _bookRequestTransactionRepository;
        private readonly ILogger<IUpdateBookRequestStatusHandler> _logger;

        public UpdateBookRequestStatusHandler(IBookRequestTransactionRepository bookRequestTransactionRepository, ILogger<IUpdateBookRequestStatusHandler> logger)
        {
            _bookRequestTransactionRepository = bookRequestTransactionRepository ?? throw new ArgumentNullException(nameof(bookRequestTransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(UpdateBookRequestStatusRequestDTO updateBookRequestStatusRequestDTO)
        {
            var updateBookRequestValidator = new UpdateBookRequestStatusValidation();
            var validationResult = await updateBookRequestValidator.ValidateAsync(updateBookRequestStatusRequestDTO);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("update book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var updateBookRequestTransaction = new BookRequestTransaction()
            {
                TransactionId = updateBookRequestStatusRequestDTO.TransactionId,
                IsActive = updateBookRequestStatusRequestDTO.IsActive,
                LastModifiedBy = updateBookRequestStatusRequestDTO.ModifiedBy
            };

            _logger.LogInformation("updateling book request.");
            var response = await _bookRequestTransactionRepository.UpdateAsync(updateBookRequestTransaction);

            if (response?.TransactionId == null)
            {
                _logger.LogError("Error updateling book request");
                throw new ApplicationException("Error updateling book request");
            }

            _logger.LogInformation("Successfully updateled book request.");

            return true;
        }
    }
}
