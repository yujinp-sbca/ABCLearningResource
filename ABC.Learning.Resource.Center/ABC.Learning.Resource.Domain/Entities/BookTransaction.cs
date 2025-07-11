using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookTransaction : AuditEntity
    {
        public required Guid TransactionId { get; set; }
        public required Guid BookId { get; set; }
        public required Guid UserId { get; set; } // Assuming UserId is a string, adjust as necessary
        public required DateTime TransactionDate { get; set; }
        public required string TransactionType { get; set; } // e.g., "Borrow", "Return"
        public string? Notes { get; set; } // Optional notes about the transaction
    }
}
