using ABC.Learning.Resource.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class UserAccount : AuditEntity
    {
        [Key]
        public required Guid UserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }        
        public required string Salt { get; set; }
        public required string HashedPassword { get; set; }
        public required bool IsAdmin { get; set; }
        public bool IsMembershipRevoked { get; set; } = false;

    }
}
