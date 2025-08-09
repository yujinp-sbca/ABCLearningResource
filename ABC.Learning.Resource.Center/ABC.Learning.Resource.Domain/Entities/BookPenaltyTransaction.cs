using ABC.Learning.Resource.Domain.Common;
using ABC.Learning.Resource.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookPenaltyTransaction : AuditEntity
    {
        [Key]
        public required Guid BookTransactionPenaltyId { get; set; }
        public Guid TransactionId { get; set; }
        public BookPenaltyType PenaltyType { get; set; } // Type of penalty (e.g., lost book, late return)
        public decimal PenaltyAmount { get; set; } // Amount of penalty
        public decimal PaymentAmount { get; set; }
        public bool IsPaid { get; set; } = false; // Indicates if the penalty has been paid

    }
}
