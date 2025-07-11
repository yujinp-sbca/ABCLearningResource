using ABC.Learning.Resource.Domain.Common;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class User : AuditEntity
    {
        public required string UserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
    }
}
