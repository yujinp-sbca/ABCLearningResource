using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public class CancelBookReservationValidation : AbstractValidator<CancelBookReservationRequestDTO>
    {
        public CancelBookReservationValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Cancel book reservation request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.TransactionId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("CancelBookReservationRequestDTO.TransactionId cannot be empty or null.");                   
                   RuleFor(a => a.UserId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("CancelBookReservationRequestDTO.UserId cannot be empty or null.");
               });
        }
    }
}
