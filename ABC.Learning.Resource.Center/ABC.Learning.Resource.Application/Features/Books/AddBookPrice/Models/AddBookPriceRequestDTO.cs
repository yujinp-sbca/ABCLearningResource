using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class AddBookPriceRequestDTO
    {
        public Guid BookId { get; set; }
        public double Price { get; set; }
        public Guid CreatedBy { get; set; } = Guid.Empty;
    }
}
