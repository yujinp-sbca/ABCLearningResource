using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class AddBookRequestValidation : AbstractValidator<AddBookRequestDTO>
    {
        public AddBookRequestValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Add book request request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.BookId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("AddBookRequestDTO.BookId cannot be empty or null.");
                   RuleFor(a => a.ModifiedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("AddBookRequestDTO.ModifiedBy cannot be empty or null.");
                   RuleFor(a => a.RequestedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("AddBookRequestDTO.ReservedBy cannot be empty or null.");
               });
        }
    }
}
