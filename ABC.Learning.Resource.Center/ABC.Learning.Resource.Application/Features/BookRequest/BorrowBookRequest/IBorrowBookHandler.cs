using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public interface IBorrowBookHandler
    {
        public Task<bool> Handle(BorrowBookRequestDTO borrowBookReservationRequest);
    }
}
