using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class Book : AuditEntity
    {
        public required Guid BookId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required DateTime PublishedDate { get; set; }
        public required string Publisher { get; set; }
        public required string Language { get; set; }
        public required string Description { get; set; }
        public required string CoverImageUrl { get; set; }
    }
}
