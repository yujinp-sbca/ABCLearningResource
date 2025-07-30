using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookReservationTransaction : AuditEntity
    {
        public required Guid TransactionId { get; set; }
        public Guid BookId { get; set; } = Guid.Empty;
        public bool IsActive { get; set; } = true; // Indicates if the reservation is currently active
        public bool IsBorrowed { get; set; } = false; // Indicates if the book is currently borrowed
    }
}
