using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IGetBookPriceByBookId
    {
        public Task<GetBookPriceByBookIdResponseDTO> Handle(Guid bookId);
    }
}
