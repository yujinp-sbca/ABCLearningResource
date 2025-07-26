using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookCategory
    {
        [Key]
        public required Guid BookCategoryId { get; set; }
        public required string Name { get; set; }
        public required bool IsActive { get; set; }
    }
}
