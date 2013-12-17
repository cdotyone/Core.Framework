using System.Net;

namespace Civic.T4.WebApi
{
    internal class ErrorMessages
    {
        public static string GetMessages(HttpStatusCode stateCode)
        {
            switch (stateCode)
            {
                case HttpStatusCode.Continue:
                    return "100 Continue";
                case HttpStatusCode.SwitchingProtocols:
                    return "101 Version Changed";
                case HttpStatusCode.OK:
                    return "200 OK";
                case HttpStatusCode.Created:
                    return "201 Created";
                case HttpStatusCode.Accepted:
                    return "202 Accepted";
                case HttpStatusCode.NonAuthoritativeInformation:
                    return "203 Non Authoritative Information";
                case HttpStatusCode.NoContent:
                    return "204 No Content";
                case HttpStatusCode.ResetContent:
                    return "205 Reset Content";
                case HttpStatusCode.PartialContent:
                    return "206 Partial Conten";
                case HttpStatusCode.Ambiguous:
                    return "300 Ambigous or to many matches";
                case HttpStatusCode.Moved:
                    return "301 Moved";
                case HttpStatusCode.Redirect:
                    return "302 Redirect";
                case HttpStatusCode.RedirectMethod:
                    return "303 Redirect";
                case HttpStatusCode.NotModified:
                    return "304 Not Modified";
                case HttpStatusCode.UseProxy:
                    return "305 Use Proxy";
                case HttpStatusCode.Unused:
                    return "306 Unused";
                case HttpStatusCode.TemporaryRedirect:
                    return "307 Temporary Redirect";
                case HttpStatusCode.BadRequest:
                    return "400 Bad Request";
                case HttpStatusCode.Unauthorized:
                    return "401 Not Authorizes";
                case HttpStatusCode.PaymentRequired:
                    return "402 Payment Requred";
                case HttpStatusCode.Forbidden:
                    return "403 Forbidden";
                case HttpStatusCode.NotFound:
                    return "404 Not Found";
                case HttpStatusCode.MethodNotAllowed:
                    return "405 Method Not Allowed";
                case HttpStatusCode.NotAcceptable:
                    return "406 Not Acceptable";
                case HttpStatusCode.ProxyAuthenticationRequired:
                    return "407 Proxy Authentication Required";
                case HttpStatusCode.RequestTimeout:
                    return "408 Request Timeout";
                case HttpStatusCode.Conflict:
                    return "409 Conflict";
                case HttpStatusCode.Gone:
                    return "410 Gone";
                case HttpStatusCode.LengthRequired:
                    return "411 LengthRequired";
                case HttpStatusCode.PreconditionFailed:
                    return "412 Precondition Failed";
                case HttpStatusCode.RequestEntityTooLarge:
                    return "413 Request Entity Too Large";
                case HttpStatusCode.RequestUriTooLong:
                    return "414 Request Uri Too Long";
                case HttpStatusCode.UnsupportedMediaType:
                    return "415 Unsupported Media Type";
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    return "416 Requested Range Not Satisfiable";
                case HttpStatusCode.ExpectationFailed:
                    return "417 Expectation Failed";
                case HttpStatusCode.InternalServerError:
                    return "500 Internal Server Error";
                case HttpStatusCode.NotImplemented:
                    return "501 Not Implemented";
                case HttpStatusCode.BadGateway:
                    return "502 BadGateway";
                case HttpStatusCode.ServiceUnavailable:
                    return "503 Service Unavailable";
                case HttpStatusCode.GatewayTimeout:
                    return "504 Gateway Timeout";
                case HttpStatusCode.HttpVersionNotSupported:
                    return "505 Http Version Not Supported";
            }

            return "Unknown";
        }
    }
}
