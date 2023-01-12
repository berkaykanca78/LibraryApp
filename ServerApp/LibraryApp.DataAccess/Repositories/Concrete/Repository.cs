using LibraryApp.Data.Common;
using LibraryApp.Data.Context;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Infrastructure.DataContext.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly LibraryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(LibraryDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                return null;
            await _dbSet.Add(entity).ReloadAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, int id)
        {
            if (entity == null)
                return null;
            TEntity exist = await _dbSet.FindAsync(id);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            }
            return exist;
        }

        public void Delete(int id)
        {
            dynamic entity = _dbSet.Find(id);
            entity.IsDeleted = true;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await _dbSet.Where(predicate).CountAsync().ConfigureAwait(false);
            }
            else
            {
                return await _dbSet.CountAsync().ConfigureAwait(false);
            }
        }


        public virtual TEntity AddOrUpdate(TEntity entity)
        {
            if (entity == null)
                return null;
            if (entity.Id > 0) //Update
            {
                TEntity exist = _dbContext.Set<TEntity>().Find(entity.Id);
                if (exist != null)
                {
                    _dbContext.Entry(exist).CurrentValues.SetValues(entity);
                }
                return exist;
            }
            else //Add
            {
                entity = _dbContext.Set<TEntity>().Add(entity).Entity;
                return entity;
            }
        }
    }
}
