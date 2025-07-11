using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Common
{
    public class AuditEntity
    {
        public required string CreatedBy { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required string LastModifiedBy { get; set; }
        public required DateTime LastModifiedDate { get; set; }
    }
}
