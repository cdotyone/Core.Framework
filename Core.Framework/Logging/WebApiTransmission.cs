using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Core.Framework.Logging
{
    public class WebApiTransmission
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Content { get; set; }

        public WebApiTransmission CreateResponse(HttpResponseMessage response)
        {
            return new WebApiTransmission
            {
                 Uri = Uri
                ,Method = Method
                ,StatusCode = Convert.ToInt32(response.StatusCode)
                ,Type = "Response"
                ,Headers = extractHeaders(response.Headers)
            };
        }

        public WebApiTransmission()
        {

        }

        public WebApiTransmission(HttpRequestMessage request)
        {
            Method = request.Method.Method;
            Uri = request.RequestUri.ToString();
            Headers = extractHeaders(request.Headers);
            Type = "Request";
        }

        private Dictionary<string, string> extractHeaders(HttpHeaders h)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var i in h.ToList())
            {
                if (i.Value != null)
                {
                    string header = string.Empty;
                    foreach (var j in i.Value)
                    {
                        header += j + " ";
                    }
                    dict.Add(i.Key, header);
                }
            }
            return dict;
        }
    }
}
