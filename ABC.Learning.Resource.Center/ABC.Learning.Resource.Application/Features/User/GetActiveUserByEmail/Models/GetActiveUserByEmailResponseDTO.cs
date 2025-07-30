using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.User
{
    public class GetActiveUserByEmailResponseDTO
    {
        public Guid UserId { get; set; } = Guid.Empty;
        
        public bool IsAdmin { get; set; } = false;
    }
}
