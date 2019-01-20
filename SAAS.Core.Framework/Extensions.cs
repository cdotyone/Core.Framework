using System;
using SAAS.Core.Framework.Configuration;

namespace SAAS.Core.Framework
{
    public static class Extensions
    {
        public static DateTime FromDB(this DateTime dte)
        {
            if (T4Config.Current.UseLocalTime)
            {
                return DateTime.SpecifyKind(dte,DateTimeKind.Local);
            }
            return DateTime.SpecifyKind(dte, DateTimeKind.Utc); 
        }

        public static DateTime? ToDB(this DateTime? dte)
        {
            if (!dte.HasValue) return null;
            if (T4Config.Current.UseLocalTime)
            {
                return dte.Value.ToLocalTime();
            }
            return dte.Value.ToUniversalTime();
        }

        public static DateTime ToDB(this DateTime dte)
        {
            if (T4Config.Current.UseLocalTime)
            {
                return dte.ToLocalTime();
            }
            return dte.ToUniversalTime();
        }

        public static DateTime? Date(this DateTime? dte)
        {
            if (!dte.HasValue) return null;
            return dte.Value.Date.ToUniversalTime();
        }

        public static string InsureUID(this string uid)
        {
            if (!string.IsNullOrEmpty(uid))
                return uid;
            return Guid.NewGuid().ToString().Replace("-","").ToUpperInvariant();
            
        }

        public static object ConvertTo(this string from,Type type)
        {
            object value = from;

            switch (nameof(type))
            {
                case nameof(Int16):
                    value = Int16.Parse(from);
                    break;
                case nameof(Int32):
                    value = Int32.Parse(from);
                    break;
                case nameof(Int64):
                    value = Int64.Parse(from);
                    break;
                case nameof(Decimal):
                    value = Decimal.Parse(from);
                    break;
                case nameof(Double):
                    value = Double.Parse(from);
                    break;
                case nameof(Single):
                    value = Single.Parse(from);
                    break;
                case nameof(Char):
                    value = Char.Parse(from);
                    break;
                case nameof(SByte):
                    value = SByte.Parse(from);
                    break;
                case nameof(Byte):
                    value = Byte.Parse(from);
                    break;
                case nameof(String):
                    if (from == "uid") value = string.Empty.InsureUID();
                    break;
                case nameof(DateTime):
                    if (from == "now") value = DateTime.UtcNow;
                    else value = DateTime.Parse(from);
                    break;
                case nameof(Boolean):
                    value = Boolean.Parse(from);
                    break;
            }

            return value;
        }
    }
}
