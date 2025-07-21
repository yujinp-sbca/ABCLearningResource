using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Services
{
    public class AddBookServiceResponseDTO
    {
        public Guid BookId { get; set; } = Guid.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public double Price { get; set; } = double.MinValue;
        public int Stock { get; set; } = int.MinValue;
    }
}
