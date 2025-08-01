using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequestPenalty
{
    public interface ISetBookPenaltyHandler
    {
        public Task<bool> Handle(SetBookPenaltyRequestDTO request);
    }
}
