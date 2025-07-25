using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public class DeleteBookByIdValidation : AbstractValidator<DeleteBookByIdRequestDTO>
    {
        public DeleteBookByIdValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Delete book request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.BookId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("DeleteBookByIdRequestDTO.BookId cannot be empty or null.");
                    RuleFor(a => a.ModifiedBy)                    
                        .NotEmpty()
                            .WithMessage("DeleteBookByIdRequestDTO.ModifiedBy cannot be empty.")
                        .NotNull()
                            .WithMessage("DeleteBookByIdRequestDTO.ModifiedBy cannot be null.");
                });
        }
    }
}
