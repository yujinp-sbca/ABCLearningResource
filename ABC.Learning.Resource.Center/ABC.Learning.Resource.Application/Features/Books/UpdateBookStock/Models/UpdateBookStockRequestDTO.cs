using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookStockRequestDTO
    {
        public Guid BookStockId = Guid.Empty;
        public Guid BookId { get; set; } = Guid.Empty;
        public int BookStock { get; set; } = 0;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
    }
}
