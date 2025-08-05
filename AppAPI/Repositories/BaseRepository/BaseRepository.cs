using AppDB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAPI.Repositories.BaseRepository
{
    public class BaseRepository <T> : IBaseRepository<T> where T : class
    {
        protected AppDBContext _entities;
        protected readonly DbSet<T> _dbset;
        public BaseRepository(AppDBContext entities)
        {
            _entities = entities;
            _dbset = _entities.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid? id)
        {
            return await _dbset.FindAsync(id);
        }
        public virtual IQueryable<T> GetQueryable()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }
        public virtual IQueryable<T> GetQueryableWithTracking()
        {
            return _dbset.AsQueryable();
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return GetQueryable().Where(predicate);
        }
        public virtual T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }
        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity).Entity;
        }
        public virtual void Update(T entity)
        {
            var entry = _entities.Entry(entity);
            entry.State = EntityState.Modified;
            if (entry.Properties.Any(p => p.Metadata.Name == "CreatedDate"))
            {
                entry.Property("CreatedDate").IsModified = false;
            }
        }
        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var entry = _entities.Entry(entity);
                entry.State = EntityState.Modified;
                if (entry.Properties.Any(p => p.Metadata.Name == "CreatedDate"))
                {
                    entry.Property("CreatedDate").IsModified = false;
                }
            }
        }
        public virtual async Task SaveAsync()
        {
            await _entities.SaveChangesAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbset.AnyAsync(predicate);
        }
        public void Delete(Expression<Func<T, bool>> filter)
        {
            var entities = _dbset.Where(filter);
            _dbset.RemoveRange(entities);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
        public async Task InsertRange(List<T> entities)
        {
            if (entities != null && entities.Any())
            {
                await _entities.Set<T>().AddRangeAsync(entities);
                await SaveAsync();
            }
        }
    }
}
