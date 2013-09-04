using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Civic.T4.WebApi
{
    public class ODataV3JsonFormatter : JsonMediaTypeFormatter
    {
        private HttpRequestMessage _request;

        public ODataV3JsonFormatter()
        {
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream,
                                                HttpContent content,
                                                System.Net.TransportContext transportContext)
        {
            var t = Task.Factory.StartNew(() =>
                {
                    var buf1 = Encoding.UTF8.GetBytes("{\"value\":");
                    writeStream.Write(buf1, 0, buf1.Length);
                }).ContinueWith(t2 => { base.WriteToStreamAsync(type, value, writeStream, content, transportContext); })
                  .ContinueWith(t3 => {
                    var buffer = new StringBuilder();
                    var inlinecount = HttpUtility.ParseQueryString(_request.RequestUri.Query).Get("$inlinecount");
                    var data = value as IQueryMetadata;

                    if (inlinecount == "allpages" && data != null && data.Count.HasValue)
                    {
                        buffer.Append(",\"odata.count\":\"");
                        buffer.Append(data.Count.Value);
                        buffer.Append("\"");
                    }

                    buffer.Append(",\"odata.metadata\":\"");
                    var url = _request.RequestUri.ToString();
                    if (url.Contains("?")) url = url.Substring(0, url.IndexOf('?'));
                    buffer.Append(url);
                    buffer.Append("/$metadata");
                    buffer.Append("\"");

                    buffer.Append("}");
                    var outbuf = Encoding.UTF8.GetBytes(buffer.ToString());
                    writeStream.Write(outbuf, 0, outbuf.Length);
                });
            return t;
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            var formatter = new ODataV3JsonFormatter {_request = request};
            return formatter;
        }
    }
}
