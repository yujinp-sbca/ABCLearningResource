using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public class BorrowBookValidation : AbstractValidator<BorrowBookRequestDTO>
    {
        public BorrowBookValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Borrow reservation request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.TransactionId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowBookRequestDTO.TransactionId cannot be empty or null.");
                    RuleFor(a => a.BookId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowBookRequestDTO.BookId cannot be empty or null.");
                    RuleFor(a => a.BorrowedBy)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowBookRequestDTO.BorrowedBy cannot be empty or null.");
                });
        }
    }
}
