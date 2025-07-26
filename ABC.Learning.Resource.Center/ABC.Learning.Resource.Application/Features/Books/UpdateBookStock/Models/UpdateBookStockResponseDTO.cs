using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookStockResponseDTO
    {
        public Guid BookId { get; set; } = Guid.Empty;
        public int BookStock { get; set; } = 0;
    }
}
