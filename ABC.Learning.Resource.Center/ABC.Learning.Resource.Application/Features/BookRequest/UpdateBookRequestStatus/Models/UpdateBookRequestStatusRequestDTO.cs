using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class UpdateBookRequestStatusRequestDTO
    {
        public Guid TransactionId { get; set; } = Guid.Empty;
        public bool IsActive { get; set; } = true; // Indicates if the reservation is currently active
        public Guid ModifiedBy { get; set; } = Guid.Empty; // User who modified the request status
    }
}
