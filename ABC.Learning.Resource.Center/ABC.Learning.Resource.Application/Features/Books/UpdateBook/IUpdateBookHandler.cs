using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IUpdateBookHandler
    {
        public Task<UpdateBookResponseDTO> Handle(UpdateBookRequestDTO updateBookRequest);
    }
}
