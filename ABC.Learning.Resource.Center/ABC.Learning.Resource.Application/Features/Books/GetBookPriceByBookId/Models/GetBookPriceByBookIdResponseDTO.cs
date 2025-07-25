using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class GetBookPriceByBookIdResponseDTO
    {
        public Guid BookId { get; set; }
        public double Price { get; set; }
    }
}
