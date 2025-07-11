using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class UserAccount : AuditEntity
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string Salt { get; set; }
        public required string HashedPassword { get; set; }
        public bool IsMembershipRevoked { get; set; } = false;

    }
}
