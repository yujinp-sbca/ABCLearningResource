using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Common
{
    public class AuditEntity
    {
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public DateTime CreatedDate { get; set; }
        public required Guid LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
