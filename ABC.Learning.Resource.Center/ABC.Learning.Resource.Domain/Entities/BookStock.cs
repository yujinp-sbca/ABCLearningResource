using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookStock : AuditEntity
    {
        [Key]
        public required Guid BookStockId { get; set; }
        public required Guid BookId { get; set; }
        public required int Quantity { get; set; }
    }
}
