using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Services.Book
{
    public class AddBookServiceRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public Guid CategoryId { get; set; } = Guid.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.MinValue;
        public string Publisher { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public double Price { get; set; } = 0.0;
        public int Stock { get; set; } = 0;
    }
}
