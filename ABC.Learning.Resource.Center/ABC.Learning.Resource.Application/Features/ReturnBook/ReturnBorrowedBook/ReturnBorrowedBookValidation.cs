using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.ReturnBook
{
    public class ReturnBorrowedBookValidation : AbstractValidator<ReturnBorrowedBookRequestDTO>
    {
        public ReturnBorrowedBookValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Return borrowed book request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.TransactionId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("ReturnBorrowedBookRequestDTO.TransactionId cannot be empty or null.");                    
                    RuleFor(a => a.ModifiedBy)
                        .NotEqual(Guid.Empty)
                            .WithMessage("ReturnBorrowedBookRequestDTO.BorrowedBy ModifiedBy be empty or null.");
                });
        }
    }
}
