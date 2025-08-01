using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.BookRequest
{
    public interface IUpdateBookRequestStatusHandler
    {
        public Task<bool> Handle(UpdateBookRequestStatusRequestDTO updateBookRequestStatusRequestDTO);
    }
}
