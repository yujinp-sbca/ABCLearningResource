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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ABCLearningResourceContext context, ILogger<BookRepository> logger)
            : base(context, logger)
        { 
        }
    }
}
