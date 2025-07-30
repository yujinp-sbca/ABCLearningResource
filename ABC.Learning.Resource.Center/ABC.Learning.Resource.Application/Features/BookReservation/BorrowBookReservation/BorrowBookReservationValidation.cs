using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookReservation
{
    public class BorrowBookReservationValidation : AbstractValidator<BorrowBookReservationRequestDTO>
    {
        public BorrowBookReservationValidation()
        {
            RuleFor(p => p)
                .NotNull()
                    .WithMessage("Borrow reservation request parameter cannot be null")
                .DependentRules(() =>
                {
                    RuleFor(a => a.TransactionId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowReservationRequestDTO.TransactionId cannot be empty or null.");
                    RuleFor(a => a.BookId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowReservationRequestDTO.BookId cannot be empty or null.");
                    RuleFor(a => a.UserId)
                        .NotEqual(Guid.Empty)
                            .WithMessage("BorrowReservationRequestDTO.UserId cannot be empty or null.");
                });
        }
    }
}
