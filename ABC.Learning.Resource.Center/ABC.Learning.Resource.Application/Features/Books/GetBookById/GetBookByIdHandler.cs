using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class GetBookByIdHandler : IGetBookByIdHandler
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<IGetBookByIdHandler> _logger;
        public GetBookByIdHandler(IBookRepository bookRepository, ILogger<IGetBookByIdHandler> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Book> Handle(Guid bookId)
        {
            if(bookId == Guid.Empty)
            {
                _logger.LogError("Book ID is empty");
                throw new ArgumentException("Book ID cannot be empty", nameof(bookId));
            }

            _logger.LogInformation($"Retrieving book with ID: {bookId}");
            var book = await _bookRepository.GetByGuidAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID: {bookId} not found");
                throw new KeyNotFoundException($"Book with ID: {bookId} not found");
            }

            return book;
        }
    }
}
