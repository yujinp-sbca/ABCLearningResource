using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookPriceValidation : AbstractValidator<UpdateBookPriceRequestDTO>
    {
        public UpdateBookPriceValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Update book price request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.BookId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("UpdateBookPriceRequestDTO.BookId cannot be empty or null.");
                    RuleFor(a => a.Price)
                        .GreaterThan(0)
                            .WithMessage("UpdateBookPriceRequestDTO.Price must be greater than zero.")
                        .NotNull()
                            .WithMessage("UpdateBookPriceRequestDTO.Price cannot be null.");
                });
        }
    }    
}
