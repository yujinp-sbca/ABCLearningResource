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
    public class BookTransactionPenaltyRepository : BaseRepository<BookPenaltyTransaction>, IBookTransactionPenaltyRepository
    {
        public BookTransactionPenaltyRepository(ABCLearningResourceContext context, ILogger<BookTransactionPenaltyRepository> logger) : base(context, logger)
        {            
        }

        public Task<Configuration?> GetConfigurationByName(string name)
        {
            return _context.Configurations
                        .Where(c => c.ConfigurationName.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefaultAsync();
        }
    }
}
