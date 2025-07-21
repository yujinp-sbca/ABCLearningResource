using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookStockRequestDTO
    {
        public Guid BookId { get; set; }
        public int Stock { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
