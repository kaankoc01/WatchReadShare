using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Domain.Entities.Common;

namespace WatchReadShare.Persistence
{
    public class GenericRepository<T, TId>(Context context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
    {

        private readonly DbSet<T> _dbSet = context.Set<T>();
        public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));

        public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

        public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);

        public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

        public async ValueTask AddAsync(T entity) => _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

    }
}
