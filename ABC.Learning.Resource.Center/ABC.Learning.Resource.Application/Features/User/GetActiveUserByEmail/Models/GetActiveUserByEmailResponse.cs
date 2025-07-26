using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.User
{
    public class GetActiveUserByEmailResponse
    {
        public string UserId { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; } = false;
    }
}
