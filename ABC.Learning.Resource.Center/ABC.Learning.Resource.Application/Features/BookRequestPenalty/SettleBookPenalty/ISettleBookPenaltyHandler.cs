using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public interface ISettleBookPenaltyHandler
    {
        public Task<bool> Handle(SettleBookPenaltyRequestDTO request);
    }
}
