using System;
using Civic.Framework.WebApi.Configuration;

namespace Civic.Framework.WebApi
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

        public static string EnsureUIDOnCreate(this string uid)
        {
            if (!string.IsNullOrEmpty(uid)) return uid;
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
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
    }
}
