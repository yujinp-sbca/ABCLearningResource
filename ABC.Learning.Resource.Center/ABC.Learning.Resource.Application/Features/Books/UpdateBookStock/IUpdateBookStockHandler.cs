using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.Books
{
    public interface IUpdateBookStockHandler
    {
        public Task<UpdateBookStockResponseDTO> Handle(UpdateBookStockRequestDTO request);
    }
}
