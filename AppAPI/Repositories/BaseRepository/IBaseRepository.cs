using System.Linq.Expressions;

namespace AppAPI.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid? id);
        IQueryable<T> GetQueryable();
        IQueryable<T> GetQueryableWithTracking();
        T Add(T entity);
        T Delete(T entity);
        void Update(T entity);
        Task SaveAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        void Delete(Expression<Func<T, bool>> filter);
        void DeleteRange(IEnumerable<T> entities);
        Task InsertRange(List<T> entities);
        void UpdateRange(IEnumerable<T> entities);
    }
}
