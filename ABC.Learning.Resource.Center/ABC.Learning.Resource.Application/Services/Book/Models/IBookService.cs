
namespace ABC.Learning.Resource.Application.Services.Book.Models
{
    public interface IBookService
    {
        public Task<AddBookServiceResponseDTO> AddBook(AddBookServiceRequestDTO addBookServiceRequestDTO);
        public Task<UpdateBookServiceResponseDTO> UpdateBook(UpdateBookServiceRequestDTO updateBookServiceRequestDTO);
        public Task<bool> DeleteBook(DeleteBookServiceRequestDTO deleteBookServiceRequestDTO);
    }
}
