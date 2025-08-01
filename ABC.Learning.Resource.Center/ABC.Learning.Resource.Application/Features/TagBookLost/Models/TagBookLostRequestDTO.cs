using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.TagBookLost
{
    public class TagBookLostRequestDTO
    {
        public Guid TransactionId { get; set; } = Guid.Empty;
        public Guid ModifiedBy { get; set; } = Guid.Empty;
    }
}
