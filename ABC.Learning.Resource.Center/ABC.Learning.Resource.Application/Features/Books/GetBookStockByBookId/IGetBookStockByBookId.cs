using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IGetBookStockByBookId
    {
        public Task<GetBookStockByBookIdResponseDTO> Handle(Guid bookId);
    }
}
