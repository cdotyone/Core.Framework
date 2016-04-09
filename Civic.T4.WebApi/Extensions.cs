using System;
using Civic.T4.WebApi.Configuration;

namespace Civic.T4.WebApi
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
            return dte;
        }

        public static DateTime ToDB(this DateTime dte)
        {
            if (T4Config.Current.UseLocalTime)
            {
                return dte.ToLocalTime();
            }
            return dte;
        }

        public static DateTime? Date(this DateTime? dte)
        {
            if (!dte.HasValue) return null;
            return dte.Value.Date;
        }
    }
}
