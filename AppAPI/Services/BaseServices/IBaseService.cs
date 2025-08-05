using System.Linq.Expressions;

namespace AppAPI.Services.BaseServices
{
    public interface IBaseService <T> where T : class
    {
        Task<T?> GetByIdAsync(Guid? id);
        Task CreateAsync(T entity);
        Task CreateAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
        IQueryable<T> GetQueryableWithTracking();
        Task DeleteAsync(IEnumerable<T> entities);
        void Delete(Expression<Func<T, bool>> filter);
        Task DeleteRange(Expression<Func<T, bool>> expression);
        void DeleteRange(IEnumerable<T> entities);
        Task InsertRange(List<T> listObj);
    }
}
