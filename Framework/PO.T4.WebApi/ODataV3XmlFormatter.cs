using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PO.T4.WebApi
{
    public class ODataV3XmlFormatter : XmlMediaTypeFormatter
    {
        private HttpRequestMessage _request;

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            if (request.RequestUri.ToString().ToLower().Contains("$metadata"))
            {
                return new ODataV3EdmxFormatter(_request);
            }
            return new ODataV3XmlFormatter {_request = request};
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                                                System.Net.TransportContext transportContext)
        {
            var data = value as IQueryMetadata;

            var t = Task.Factory.StartNew(() =>
                {
                }).ContinueWith(t2 =>
                    {
                        if (data != null)
                        {
                            var url = _request.RequestUri.ToString();
                            if (url.Contains("?")) url = url.Substring(0, url.IndexOf('?'));
                            url += "/$metadata";

                            Type listT = typeof(QueryMetadataXml<>).MakeGenericType(new[] { data.Type });
                            value = Activator.CreateInstance(listT, value, url);

                            type = value.GetType();
                        }

                        base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
                    });
            return t;
        }
    }
}