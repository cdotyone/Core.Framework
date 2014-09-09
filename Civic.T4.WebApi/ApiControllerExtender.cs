using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Civic.Core.Logging;
using Civic.T4.WebApi.Security;

namespace Civic.T4.WebApi
{
    public static class ApiControllerExtender
    {
        public static Token GetToken(this ApiController controller)
        {
            IEnumerable<string> values;
            if (controller.Request.Headers.TryGetValues("Authorization", out values))
            {
                var tokenVal = values.First();
                try
                {
                    if (!string.IsNullOrEmpty(tokenVal))
                    {
                        var token = Token.Parse(tokenVal);
                        if (!token.IsExpired) return token;
                    }
                }
                catch (Exception ex)
                {
                    Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex);
                    Logger.LogInformation(LoggingBoundaries.ServiceBoundary, "token: " + tokenVal);
                }
            }

            return null;
        }
    }
}
