using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public interface IAddBookReservationHandler
    {
        public Task<AddBookReservationResponseDTO> Handle(AddBookReservationRequestDTO addBookReservationRequest);
    }
}
