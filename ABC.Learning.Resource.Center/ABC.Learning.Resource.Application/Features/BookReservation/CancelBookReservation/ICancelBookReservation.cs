using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public interface ICancelBookReservation
    {
        public Task<bool> Handle(CancelBookReservationRequestDTO cancelBookReservationRequest);
    }
}
