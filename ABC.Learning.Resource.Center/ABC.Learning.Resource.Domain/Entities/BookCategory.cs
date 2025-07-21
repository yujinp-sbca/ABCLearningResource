using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookCategory
    {
        public required Guid BookCategoryId { get; set; }
        public required string Name { get; set; }
        public required bool IsActive { get; set; }
    }
}
