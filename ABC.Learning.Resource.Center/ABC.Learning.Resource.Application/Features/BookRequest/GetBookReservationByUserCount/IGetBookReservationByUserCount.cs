using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public interface IGetBookReservationByUserCount
    {
        public Task<int> Handle(Guid userId);
    }
}
