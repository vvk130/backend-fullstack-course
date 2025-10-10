using System.Linq.Expressions;

namespace GameModel
{
public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IRepository<T> _repository;
    
    public GenericService(IRepository<T> repository)
    {
        _repository = repository;
    }
    
    public async Task<T?> GetByIdAsync(Guid id)
        => await _repository.GetByIdAsync(id);
    
    public async Task<IEnumerable<T>> GetAllAsync()
        => await _repository.GetAllAsync();
    
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _repository.FindAsync(predicate);
    
    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }
    
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _repository.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }
    
    public virtual async Task RemoveAsync(T entity)
    {
        _repository.Remove(entity);
        await _repository.SaveChangesAsync();
    }
    
    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        _repository.RemoveRange(entities);
        await _repository.SaveChangesAsync();
    }
    
    public async Task<int> SaveChangesAsync()
        => await _repository.SaveChangesAsync();
}
}
