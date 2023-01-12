using LibraryApp.Data.Common;
using LibraryApp.Data.Context;
using LibraryApp.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync(bool withoutAudit = false, bool withoutAuditLog = false);

        void Complete();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;

        void BeginTransaction();

        void CommitTransaction();

        void RolbackTransaction();
    }
}
