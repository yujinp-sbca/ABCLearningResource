using ABC.Learning.Resource.Application.Features.Books;

namespace ABC.Learning.Resource.Application.Services
{
    public interface IBookService
    {
        public Task<AddBookServiceResponseDTO> AddBook(AddBookServiceRequestDTO bookServiceRequestDTO);
    }
}
