namespace ABC.Learning.Resource.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        public Task<T> GetByGuidAsync(Guid id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
