using System;
using Framework.Core.Configuration;

namespace Framework.Core
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
    }
}
