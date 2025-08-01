using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class BookBorrowTransaction : AuditEntity
    {
        public required Guid TransactionId { get; set; }
        public Guid BookId { get; set; } = Guid.Empty;        
        public Guid BorrowedBy { get; set; } = Guid.Empty; // User who borrowed the book
        public DateTime BorrowedDate { get; set; } = DateTime.Now; // Date when the book was borrowed
        public DateTime? DueDate { get; set; } // Optional due date for returning the book        
        public bool IsActive { get; set; } = false; // Indicates if the transaction is still active                
        public DateTime? ReturnedDate { get; set; } // Optional date when the book was returned
        public bool IsLost { get; set; } = false; // Indicates if the book is lost
    }
}
