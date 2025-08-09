using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SettleBookPenaltyValidation : AbstractValidator<SettleBookPenaltyRequestDTO>
    {
        public SettleBookPenaltyValidation()
        {
            RuleFor(p => p)
               .NotNull()
                   .WithMessage("SettleBookPenaltyRequestDTO parameter cannot be null")
               .DependentRules(() =>
               {
                   RuleFor(a => a.BookTransactionPenaltyId)
                       .NotEqual(Guid.Empty)
                           .WithMessage("SettleBookPenaltyRequestDTO.BookTransactionPenaltyId cannot be empty or null.");
                   RuleFor(a => a.ModifiedBy)
                       .NotEqual(Guid.Empty)
                           .WithMessage("SettleBookPenaltyRequestDTO.ModifiedBy cannot be empty or null.");
                   RuleFor(a => a.PenaltyType)
                    .NotEqual(null)
                        .WithMessage("SettleBookPenaltyRequestDTO.PenaltyType cannot be empty or null.");
                   RuleFor(a => a.PaymentAmount)
                        .LessThan(0)
                            .WithMessage("SettleBookPenaltyRequestDTO.PaymentAmount cannot be empty or null.");
               });
        }
    }
}
