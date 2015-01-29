using System;
using Civic.T4.WebApi.Configuration;

namespace Civic.T4.WebApi
{
    public static class Extensions
    {
        public static DateTime FromDB(this DateTime dte)
        {
            if (T4WebApiSection.Current.UseLocalTime)
            {
                return dte.ToUniversalTime();
            }
            return dte;
        }

        public static DateTime? ToDB(this DateTime? dte)
        {
            if (!dte.HasValue) return null;
            if (T4WebApiSection.Current.UseLocalTime)
            {
                return dte.Value.ToLocalTime();
            }
            return dte;
        }

        public static DateTime ToDB(this DateTime dte)
        {
            if (T4WebApiSection.Current.UseLocalTime)
            {
                return dte.ToLocalTime();
            }
            return dte;
        }
    }
}
