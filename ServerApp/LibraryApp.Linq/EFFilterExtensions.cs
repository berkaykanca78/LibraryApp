using LibraryApp.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryApp.Linq
{
    public static class EFFilterExtensions
    {
        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFFilterExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IBaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
