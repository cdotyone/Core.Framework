using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

// ReSharper disable ImplicitlyCapturedClosure

namespace Civic.Framework.WebApi
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
                                                TransportContext transportContext)
        {
            var data = value as IQueryMetadata;

            if (data != null && data.StatusCode != HttpStatusCode.OK)
            {
                HttpContext.Current.Response.StatusCode = (int)data.StatusCode;

                var error = data.StatusMessage;
                if (string.IsNullOrEmpty(error)) error = data.StatusCode.ToString();

                var t3 = Task.Factory.StartNew(() =>
                {
                }).ContinueWith(t4 =>
                {
                    var tdict = new List<Dictionary<string, string>> { new Dictionary<string, string> { { "error", error } } };
                    base.WriteToStreamAsync(tdict.GetType(), tdict, writeStream, content, transportContext);
                });

                return t3;
            }

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