using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class AddBookRequestDTO
    {
        public Guid BookId { get; set; } = Guid.Empty;
        public Guid RequestedBy { get; set; } = Guid.Empty;
        public Guid ModifiedBy { get; set; } = Guid.Empty; // User who modified the request
        public bool IsReservation { get; set; } = false; // Indicates if the request is for a reservation
    }
}
