using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class UpdateBookRequestStatusValidation : AbstractValidator<UpdateBookRequestStatusRequestDTO>
    {
        public UpdateBookRequestStatusValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Update book request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.TransactionId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("UpdateBookRequestStatusRequestDTO.TransactionId cannot be empty or null.");
                   RuleFor(a => a.ModifiedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("UpdateBookRequestStatusRequestDTO.UserId cannot be empty or null.");
               });
        }
    }
}
