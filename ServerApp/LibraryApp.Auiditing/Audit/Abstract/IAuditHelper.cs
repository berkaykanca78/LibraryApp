using LibraryApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace LibraryApp.Auiditing.Auiditing.Abstract
{
    public interface IAuditHelper
    {
        List<Audit> AddAuditLog(ChangeTracker changeTracker);
    }
}
