using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Common
{
    public class AuditEntity
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
