using ABC.Learning.Resource.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class User : AuditEntity
    {
        [Key]
        public required string UserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
    }
}
