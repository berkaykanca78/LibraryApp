using LibraryApp.Data.Common;
using LibraryApp.Data.Context;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.DataAccess.Repositories.Concrete;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LibraryDbContext _context;

        private Dictionary<Type,object> repositories;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int> CompleteAsync(bool withoutAudit = false, bool withoutAuditLog = false)
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual void Complete()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RolbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)repositories[type];
        }
    }
}
