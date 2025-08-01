using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.ReturnBook
{
    public interface IReturnBorrowedBook
    {
        public Task<bool> Handle(ReturnBorrowedBookRequestDTO request);
    }
}
