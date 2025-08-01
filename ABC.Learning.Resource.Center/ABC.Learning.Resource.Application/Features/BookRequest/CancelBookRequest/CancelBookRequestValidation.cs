using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class CancelBookRequestValidation : AbstractValidator<CancelBookRequestDTO>
    {
        public CancelBookRequestValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Cancel book request request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.TransactionId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("CancelBookRequestDTO.TransactionId cannot be empty or null.");                   
                   RuleFor(a => a.ModifiedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("CancelBookReservationRequestDTO.UserId cannot be empty or null.");
               });
        }
    }
}
