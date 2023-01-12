using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Common.Extensions
{
    public static class NullExtensions
    {
        public static T GetValueOrDefault<T>(this T entity) where T : new()
        {
            var deger = entity == null ? new T() : entity;
            return deger;
        }
    }
}
