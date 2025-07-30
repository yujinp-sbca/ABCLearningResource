using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public class AddBookReservationValidation : AbstractValidator<AddBookReservationRequestDTO>
    {
        public AddBookReservationValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Update book reservation request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.BookId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("AddBookReservationRequestDTO.BookId cannot be empty or null.");
                   RuleFor(a => a.UserId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("AddBookReservationRequestDTO.UserId cannot be empty or null.");
               });
        }
    }
}
