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
    public class BookTransactionPenalty : AuditEntity
    {
        [Key]
        public required Guid BookTransactionPenaltyId { get; set; }
        public required Guid TransactionId { get; set; }
        public BookPenaltyType PenaltyType { get; set; } // Type of penalty (e.g., lost book, late return)
        public required decimal PenaltyAmount { get; set; } // Amount of penalty
        public bool IsPaid { get; set; } = false; // Indicates if the penalty has been paid

    }
}
