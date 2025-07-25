using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class UpdateBookStockValidation : AbstractValidator<UpdateBookStockRequestDTO>
    {
        public UpdateBookStockValidation()
        {
            RuleFor(p => p)
                 .NotNull()
                     .WithMessage("Update book stock request parameter cannot be null")
                 .DependentRules(() =>
                 {
                     RuleFor(a => a.BookId)
                         .NotEqual(Guid.Empty)
                             .WithMessage("UpdateBookStockRequestDTO.BookId cannot be empty or null.");
                     RuleFor(a => a.BookStock)
                         .GreaterThan(0)
                             .WithMessage("UpdateBookStockRequestDTO.Stock must be greater than zero.")
                         .NotNull()
                             .WithMessage("UpdateBookStockRequestDTO.Stock cannot be null.");
                 });
        }
    }
}
