using ABC.Learning.Resource.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Application.Contracts.Persistence
{
    public interface IUserAccountRepository : IAsyncRepository<UserAccount>
    {
        public Task<UserAccount> GetActiveUserByEmail(Guid userId);
    }
}
