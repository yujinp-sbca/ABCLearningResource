using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        
        public UserAccountRepository(ABCLearningResourceContext context, ILogger<UserAccountRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<UserAccount> GetActiveUserByEmail(Guid userId)
        {
            var result = await _context.UserAccounts
                            .Where(u => u.UserId == userId && !u.IsMembershipRevoked)
                            .FirstOrDefaultAsync();

            return result;
        }
        // Additional methods specific to UserAccount can be implemented here
    }

}
