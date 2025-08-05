using AppAPI.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace AppAPI.Services.BaseServices
{
    public class BaseService <T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T?> GetByIdAsync(Guid? guid)
        {
            return await _repository.GetByIdAsync(guid);
        }
        public virtual async Task CreateAsync(T entity)
        {
            _repository.Add(entity);
            await _repository.SaveAsync();
        }
        public virtual async Task CreateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Add(entity);
            }
            await _repository.SaveAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
        public virtual async Task UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Update(entity);
            }
            await _repository.SaveAsync();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _repository.Delete(entity);
            await _repository.SaveAsync();
        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            if (entities != null && entities.Any())
            {
                _repository.DeleteRange(entities);
            }
        }
        public virtual async Task DeleteRange(Expression<Func<T, bool>> expression)
        {
            var entities = await _repository.GetQueryable().Where(expression).ToListAsync();
            if (entities != null && entities.Any())
            {
                _repository.DeleteRange(entities);
            }
        }
        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }
        public IQueryable<T> GetQueryableWithTracking()
        {
            return _repository.GetQueryableWithTracking();
        }
        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Delete(entity);
            }
            await _repository.SaveAsync();
        }
        public void Delete(Expression<Func<T, bool>> filter)
        {
            //_repository.Delete(filter);
        }
        public async Task InsertRange(List<T> listObj)
        {
            await _repository.InsertRange(listObj);
        }

    }
}
