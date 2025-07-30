using ABC.Learning.Resource.Domain.Common;
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
        public required Guid BookTransactionId { get; set; }
        public required decimal PenaltyAmount { get; set; } // Amount of penalty

    }
}
