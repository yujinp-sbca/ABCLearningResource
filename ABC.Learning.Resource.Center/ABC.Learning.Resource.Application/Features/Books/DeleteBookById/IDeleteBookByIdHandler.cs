using ABC.Learning.Resource.Application.Features.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IDeleteBookByIdHandler
    {
        public Task<bool> Handle(DeleteBookByIdRequestDTO deleteBookByIdRequestDTO);
    }
}
