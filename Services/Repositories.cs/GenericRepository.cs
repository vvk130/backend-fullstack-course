using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace GameModel
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public GenericRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
            => await _dbSet.Where(predicate).ToListAsync(ct);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            await _dbSet.AddRangeAsync(entities, ct);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task<PaginatedResult<TDto>> GetPaginatedAsync<TDto>(PaginationRequest request, Expression<Func<T, bool>>? predicate = null)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (predicate is not null){
                query = query.Where(predicate);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(e => EF.Property<object>(e, "Id"))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedResult<TDto>(items, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<PaginatedResult<SalesAdShortDto<TDto>>> GetPaginatedAdsWithItemsAsync<TEntity, TDto>(
            ItemType type,
            int pageNumber,
            int pageSize)
            where TEntity : class
        {
            var ads = _context.Set<SalesAd>()
                .Where(a => a.ItemType == type)
                .OrderByDescending(a => a.EndTime);

            var totalCount = await ads.CountAsync();

            var paginated = await ads
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<SalesAdShortDto<TDto>>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedResult<SalesAdShortDto<TDto>>(paginated, totalCount, pageNumber, pageSize);
        }
    }
}
