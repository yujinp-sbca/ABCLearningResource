using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Services.Book
{
    public class DeleteBookServiceRequestDTO
    {
        public Guid BookId { get; set; } = Guid.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
