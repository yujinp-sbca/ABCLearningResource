using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class BorrowBookRequestDTO
    {
        public Guid TransactionId { get; set; } = Guid.Empty;
        public Guid BookId { get; set; } = Guid.Empty;
        public Guid BorrowedBy { get; set; } = Guid.Empty;
        public bool IsBorrowed { get; set; } = false;

    }
}
