using ABC.Learning.Resource.Application.Contracts.Persistence;
using ABC.Learning.Resource.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BookPriceRepository : BaseRepository<BookPrice>, IBookPriceRepository
    {
        public BookPriceRepository(ABCLearningResourceContext context, ILogger<BaseRepository<BookPrice>> logger) : base(context, logger)
        {
        }
    }
}
