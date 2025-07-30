using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public interface IBorrowBookReservationHandler
    {
        public Task<bool> Handle(BorrowBookReservationRequestDTO borrowBookReservationRequest);
    }
}
