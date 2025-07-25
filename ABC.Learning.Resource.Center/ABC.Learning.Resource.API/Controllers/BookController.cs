using ABC.Learning.Resource.Application.Services.Book;
using ABC.Learning.Resource.Application.Services.Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABC.Learning.Resource.API.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        //AddBook
        [HttpPost("api/v1/books/add")]
        public async Task<IActionResult> AddBook([FromBody] AddBookServiceRequestDTO addBookServiceRequestDTO)
        {
            if (addBookServiceRequestDTO == null)
            {
                _logger.LogError("Invalid book service request DTO.");
                return BadRequest("Invalid book service request.");
            }

            try
            {
                
                var response = await _bookService.AddBook(addBookServiceRequestDTO);
                
                if (response == null || response.BookId == Guid.Empty)
                {
                    _logger.LogError("Failed to add book.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add book.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookServiceRequestDTO updateBookServiceRequestDTO)
        {
            if (updateBookServiceRequestDTO == null)
            {
                _logger.LogError("Invalid book service request DTO.");
                return BadRequest("Invalid book service request.");
            }

            try
            {

                var response = await _bookService.UpdateBook(updateBookServiceRequestDTO);

                if (response == null || response.BookId == Guid.Empty)
                {
                    _logger.LogError("Failed to update book.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update book.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookServiceRequestDTO deleteBookServiceRequestDTO)
        {
            if (deleteBookServiceRequestDTO == null)
            {
                _logger.LogError("Invalid book service request DTO.");
                return BadRequest("Invalid book service request.");
            }

            try
            {

                var response = await _bookService.DeleteBook(deleteBookServiceRequestDTO);

                if (response == null)
                {
                    _logger.LogError("Failed to delete book.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete book.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
