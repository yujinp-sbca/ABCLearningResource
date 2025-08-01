using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using ABC.Learning.Resource.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.ReturnBook
{
    public class ReturnBorrowedBook : IReturnBorrowedBook
    {
        private readonly IBookBorrowTransactionRepository _bookBorrowTransactionRepository;
        private readonly ILogger<IReturnBorrowedBook> _logger;

        public ReturnBorrowedBook(IBookBorrowTransactionRepository bookBorrowTransactionRepository, ILogger<IReturnBorrowedBook> logger)
        {
            _bookBorrowTransactionRepository = bookBorrowTransactionRepository ?? throw new ArgumentNullException(nameof(bookBorrowTransactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ReturnBorrowedBookRequestDTO request)
        {
            var returnBorrowedBookValidator = new ReturnBorrowedBookValidation();
            var validationResult = await returnBorrowedBookValidator.ValidateAsync(request);
            if ((!validationResult.IsValid))
            {
                string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Borrow book request failed: {errors}", validationErrors);
                throw new ValidationException(validationResult);
            }

            var bookBorrowTransaction = new BookBorrowTransaction()
            {
                TransactionId = request.TransactionId,
                IsActive = false,
                LastModifiedBy = request.ModifiedBy
            };

            _logger.LogInformation("Processing return book");
            var response = await _bookBorrowTransactionRepository.UpdateAsync(bookBorrowTransaction);

            if(response?.TransactionId == Guid.Empty)
            {
                _logger.LogError("Error processing return of book.");
                throw new ApplicationException("Error processing return of book");
            }

            _logger.LogInformation("Successfully returned book");

            return true;
        }
    }
}
