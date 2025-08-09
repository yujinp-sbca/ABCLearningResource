using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SettleBookPenaltyHandler : ISettleBookPenaltyHandler
    {
        private readonly IBookTransactionPenaltyRepository _bookTransactionPenaltyRepository;
        private readonly ILogger<ISettleBookPenaltyHandler> _logger;

        public SettleBookPenaltyHandler(IBookTransactionPenaltyRepository bookTransactionPenaltyRepository, ILogger<ISettleBookPenaltyHandler> logger)
        {
            _bookTransactionPenaltyRepository = bookTransactionPenaltyRepository ?? throw new ArgumentNullException(nameof(bookTransactionPenaltyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SettleBookPenaltyRequestDTO request)
        {
            var settleBookPenaltyValidator = new SettleBookPenaltyValidation();
            var validationResult = await settleBookPenaltyValidator.ValidateAsync(request);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("update book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookPenaltyTransaction = new BookPenaltyTransaction()
            {
                BookTransactionPenaltyId = request.BookTransactionPenaltyId,
                PenaltyType = request.PenaltyType,
                PaymentAmount = request.PaymentAmount,
                IsPaid = true,
                LastModifiedBy = request.ModifiedBy
            };

            _logger.LogInformation("Settling penalty.");
            var response = await _bookTransactionPenaltyRepository.UpdateAsync(bookPenaltyTransaction);
            
            if(response?.BookTransactionPenaltyId == Guid.Empty)
            {
                _logger.LogError("Error settling penalty.");
                throw new ApplicationException("Error settling penalty.");
            }

            _logger.LogInformation("Successfully settled penalty");

            return true;
        }
    }
}
