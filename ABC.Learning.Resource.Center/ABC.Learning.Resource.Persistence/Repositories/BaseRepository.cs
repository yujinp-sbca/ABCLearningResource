using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ABCLearningResourceContext _context;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ABCLearningResourceContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception err)
            {
                throw new ApplicationException(err.Message);
            }
        }
    }
}
