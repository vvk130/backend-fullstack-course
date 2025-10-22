using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameModel
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        Task<PaginatedResult<TDto>> GetPaginatedAsync<TDto>(PaginationRequest request);
    }
}
