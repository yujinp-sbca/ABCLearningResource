using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Features.TagBookLost
{
    public interface ITagBookLostHandler
    {
        public Task<bool> Handle(TagBookLostRequestDTO request);
    }
}
