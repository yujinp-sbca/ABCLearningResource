using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookPriceRequestDTO
    {
        public Guid BookPriceId { get; set; } = Guid.Empty;
        public Guid BookId { get; set; } = Guid.Empty;
        public double Price { get; set; } = 0.0;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
        public bool IsActive { get; set; } = true;
    }
}
