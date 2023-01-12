using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.DataContext.Abstract
{
    public interface IDataContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesWithoutAuditAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsyncWithoutAuditLogAsync(CancellationToken cancellationToken = default);
    }
}
