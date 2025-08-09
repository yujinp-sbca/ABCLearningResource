using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Application.Features.BookRequest;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Domain.Enum;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SetBookPenaltyHandler : ISetBookPenaltyHandler
    {
        private readonly IBookTransactionPenaltyRepository _bookTransactionPenaltyRepository;
        private readonly ILogger<ISetBookPenaltyHandler> _logger;

        public SetBookPenaltyHandler(IBookTransactionPenaltyRepository bookTransactionPenaltyRepository, ILogger<ISetBookPenaltyHandler> logger)
        {
            _bookTransactionPenaltyRepository = bookTransactionPenaltyRepository ?? throw new ArgumentNullException(nameof(bookTransactionPenaltyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SetBookPenaltyRequestDTO request)
        {
            var setBookLostPenaltyValidator = new SetBookPenaltyValidation();
            var validationResult = await setBookLostPenaltyValidator.ValidateAsync(request);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("update book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookTransactionPenalty = new BookPenaltyTransaction()
            {
                BookTransactionPenaltyId = Guid.NewGuid(),
                TransactionId = request.TransactionId,                
                PenaltyType = request.PenaltyType,
                PenaltyAmount = request.PenaltyAmount,
                CreatedBy = request.ReportedBy,
                LastModifiedBy = request.ReportedBy
            };

            return true;
        }
    }
}
