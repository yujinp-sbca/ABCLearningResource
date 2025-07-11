using ABC.Learning.Resource.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IGetBookByIdHandler
    {
        public Task<Book> Handle(Guid bookId);
    }
}
