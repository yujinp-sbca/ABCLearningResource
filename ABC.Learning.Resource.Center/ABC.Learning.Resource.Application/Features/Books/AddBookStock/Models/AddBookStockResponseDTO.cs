using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookStockResponseDTO
    {
        public Guid BookId { get; set; }
        public int Stock { get; set; }                    
    }
}
