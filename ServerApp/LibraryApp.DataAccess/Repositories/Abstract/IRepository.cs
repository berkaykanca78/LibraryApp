using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, int id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        void Delete(int id);
        TEntity AddOrUpdate(TEntity entity);
    }
}
