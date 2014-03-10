using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable ImplicitlyCapturedClosure
namespace Civic.T4.WebApi
{
    public class ODataV3JsonFormatter : JsonMediaTypeFormatter
    {
        private HttpRequestMessage _request;

        public ODataV3JsonFormatter()
        {
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            this.UseDataContractJsonSerializer = true;
        }

	    public Task BaseWriteToStreamAsync(Type type, object value, Stream writeStream,
	                                            HttpContent content,
	                                            TransportContext transportContext)
	    {
			return base.WriteToStreamAsync(type, value, writeStream, content, transportContext); 
	    }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream,
                                                HttpContent content,
                                                TransportContext transportContext)
        {
			var data = value as IQueryMetadata;

            if (data != null && data.StatusCode != HttpStatusCode.OK)
            {
                HttpContext.Current.Response.StatusCode = (int)data.StatusCode;
                var error = data.StatusMessage;
                if (string.IsNullOrEmpty(error)) error = data.StatusCode.ToString();

                var tdict = new List<Dictionary<string, string>> {new Dictionary<string, string> {{"error", error}}};
                var t3 = Task.Factory.StartNew(() =>
                    {
                        var buf1 = Encoding.UTF8.GetBytes("{\"value\":");
                        writeStream.Write(buf1, 0, buf1.Length);
                    })
                    .ContinueWith(t4 => { base.WriteToStreamAsync(type, tdict, writeStream, content, transportContext); })
                    .ContinueWith(t5 => {
                        var outbuf = Encoding.UTF8.GetBytes("}");
                        writeStream.Write(outbuf, 0, outbuf.Length);
                    });
                return t3;
            }

            var url = _request.RequestUri.ToString();
	        if (url.Contains("?")) url = url.Substring(0, url.IndexOf('?'));
	        var query = HttpUtility.ParseQueryString(_request.RequestUri.Query);
			
			var inlinecount = query.Get("$inlinecount");
	        var metadata = url.EndsWith("/$metadata");
			if (metadata)
			{
				if (data != null && data.HasMetaDataAction)
				{
					return Task.Factory.StartNew(() => data.OnMetaRequest(this, data, writeStream, content, transportContext));
				}
				return Task.Factory.StartNew(() =>
					{
						var buf1 = Encoding.UTF8.GetBytes("{\"error\":\"no metadata available using json\"}");
						writeStream.Write(buf1, 0, buf1.Length);
					});
			}

			var t = Task.Factory.StartNew(() =>
                {
                    var buf1 = Encoding.UTF8.GetBytes("{\"value\":");
                    writeStream.Write(buf1, 0, buf1.Length);
                }).ContinueWith(t2 => { base.WriteToStreamAsync(type, value, writeStream, content, transportContext); })
                  .ContinueWith(t3 => {
                    var buffer = new StringBuilder();

                    if (inlinecount == "allpages" && data != null && data.Count.HasValue)
                    {
                        buffer.Append(",\"odata.count\":\"");
                        buffer.Append(data.Count.Value);
                        buffer.Append("\"");
                    }

		            buffer.Append(",\"odata.metadata\":\"");
		            buffer.Append(url);
		            if (!url.EndsWith("/")) buffer.Append("/");
		            buffer.Append("$metadata");
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
