using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookPrice : AuditEntity
    {
        public required Guid BookPriceId { get; set; }
        public required Guid BookId { get; set; }
        public required double Price { get; set; }
    }
}
