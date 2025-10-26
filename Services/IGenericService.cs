using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameModel
{
    public interface IGenericService<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        Task<PaginatedResult<TDto>> GetPaginatedAsync<TDto>(PaginationRequest request, Expression<Func<T, bool>>? predicate = null);
        Task<PaginatedResult<SalesAdShortDto<TDto>>> GetPaginatedAdsWithItemsAsync<TEntity, TDto>(
            ItemType type, int pageNumber, int pageSize) 
            where TEntity : class;
    }
}
