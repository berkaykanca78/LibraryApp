using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LibraryApp.Common.Extensions
{
    public static partial class EnumExtensions
    {
        public static int GetValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string DisplayName(this Enum value)
        {
            string outString = string.Empty;

            if (value != null)
            {
                Type enumType = value.GetType();
                string enumValue = Enum.GetName(enumType, value);
                if (enumValue != null)
                {
                    MemberInfo member = enumType.GetMember(enumValue)[0];

                    object[] attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attrs.Length == 0)
                    {
                        return "";
                    }
                    outString = ((DisplayAttribute)attrs[0]).Name;

                    if (((DisplayAttribute)attrs[0]).ResourceType != null)
                    {
                        outString = ((DisplayAttribute)attrs[0]).GetName();
                    }
                }
            }
            return outString;
        }

        public static string DisplayShortName(this Enum value)
        {
            var outString = String.Empty;

            if (value != null)
            {
                Type enumType = value.GetType();
                var enumValue = Enum.GetName(enumType, value);
                if (enumValue != null)
                {
                    MemberInfo member = enumType.GetMember(enumValue)[0];

                    var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attrs.Length == 0)
                    {
                        return "";
                    }
                    outString = ((DisplayAttribute)attrs[0]).ShortName;

                    if (((DisplayAttribute)attrs[0]).ResourceType != null)
                    {
                        outString = ((DisplayAttribute)attrs[0]).GetName();
                    }
                }
            }

            return outString;
        }

        public static List<TEnum> GetEnumToList<TEnum>(Func<TEnum, bool> exceptedValues = null)
        {
            if (!typeof(TEnum).IsEnum) throw new InvalidOperationException();
            var list = Enum.GetValues(typeof(TEnum))
                      .Cast<TEnum>()
                      .Where(p => p.GetHashCode() != 0)
                      .Select(v => v);

            if (exceptedValues != null)
            {
                list = list.Where(exceptedValues);
            }

            return list.ToList();
        }

        public static string GetEnumName(this Enum value)
        {
            var outString = String.Empty;

            if (value != null)
            {
                Type enumType = value.GetType();
                var enumValue = Enum.GetName(enumType, value);
                return enumValue;
            }
            return outString;
        }

    }
}
