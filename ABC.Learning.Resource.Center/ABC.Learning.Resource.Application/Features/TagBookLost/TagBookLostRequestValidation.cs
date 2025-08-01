using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.TagBookLost
{
    public class TagBookLostRequestValidation : AbstractValidator<TagBookLostRequestDTO>
    {
        public TagBookLostRequestValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("Tag book lost request parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.TransactionId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("TagBookLostRequestDTO.TransactionId cannot be empty or null.");
                   RuleFor(a => a.ModifiedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("TagBookLostRequestDTO.ModifiedBy cannot be empty or null.");
               });
        }
    }
}
