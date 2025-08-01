using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SetBookPenaltyValidation : AbstractValidator<SetBookPenaltyRequestDTO>
    {
        public SetBookPenaltyValidation()
        {
            RuleFor(p => p)
              .NotNull()
                  .WithMessage("Set book lost penalty request parameter cannot be null")
              .DependentRules(() =>
              {
                  RuleFor(a => a.TransactionId)
                      .NotEqual(Guid.Empty)
                          .WithMessage("SetBookPenaltyRequestDTO.TransactionId cannot be empty or null.");
                  RuleFor(a => a.ReportedBy)
                      .NotEqual(Guid.Empty)
                          .WithMessage("SetBookPenaltyRequestDTO.UserId cannot be empty or null.");
                  RuleFor(a => a.PenaltyType)
                      .NotEqual(null)
                          .WithMessage("SetBookPenaltyRequestDTO.PenaltyType cannot be empty or null.");
                  RuleFor(a => a.PenaltyAmount)
                      .NotEqual(0)
                            .WithMessage("SetBookPenaltyRequestDTO.PenaltyAmount cannot be zero.")
                      .LessThan(0)
                          .WithMessage("SetBookPenaltyRequestDTO.PenaltyAmount cannot be less than 0.");
              });
        }
    }
}
