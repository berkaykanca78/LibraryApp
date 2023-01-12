using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropValue(this object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName)?.GetValue(src, null);
            }
            catch
            {
                return null;
            }
        }
    }
}
