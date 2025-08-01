using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;


namespace ABC.Learning.Resource.Application.Features.TagBookLost
{
    public class TagBookLostHandler : ITagBookLostHandler
    {
        private readonly IBookBorrowTransactionRepository _bookBorrowTransactionRepository;
        private readonly ILogger<ITagBookLostHandler> _logger;

        public TagBookLostHandler(IBookBorrowTransactionRepository bookBorrowTransactionRepository, ILogger<ITagBookLostHandler> logger)
        {
            _bookBorrowTransactionRepository = bookBorrowTransactionRepository ?? throw new ArgumentNullException(nameof(bookBorrowTransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(TagBookLostRequestDTO request)
        {
            var tagBookLostValidator = new TagBookLostRequestValidation();
            var validationResult = await tagBookLostValidator.ValidateAsync(request);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("update book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookBorrowTransaction = new BookBorrowTransaction()
            {
                TransactionId = request.TransactionId,
                LastModifiedBy = request.ModifiedBy,
                IsLost = true
            };

            _logger.LogInformation("Tagging book as lost.");
            var response = await _bookBorrowTransactionRepository.UpdateAsync(bookBorrowTransaction);
            
            if(response?.TransactionId == Guid.Empty)
            {
                _logger.LogError("Error tagging book as lost");
                throw new ApplicationException("Error tagging book as lost");
            }

            _logger.LogInformation("Successfully tagged book as lost.");

            return true;
        }
    }
}
