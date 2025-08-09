using ABC.Learning.Resource.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public class SettleBookPenaltyRequestDTO
    {
        public Guid BookTransactionPenaltyId { get; set; } = Guid.Empty;
        public BookPenaltyType PenaltyType { get; set; }
        public Guid ModifiedBy { get; set; } = Guid.Empty;
        public decimal PaymentAmount = decimal.Zero;
    }
}
