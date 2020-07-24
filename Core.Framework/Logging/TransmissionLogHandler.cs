using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Core.Logging;

namespace Core.Framework.Logging
{
    public class TransmissionLogHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var trackingGUID = Guid.NewGuid().ToString();
            var logMetadata = new WebApiTransmission(request);
            logMetadata.Content = request.Content.ReadAsStringAsync().Result;
            Logger.LogTransmission(trackingGUID, JsonConvert.SerializeObject(logMetadata, Formatting.Indented));

            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                logMetadata = logMetadata.CreateResponse(task.Result);
                logMetadata.Content = task.Result.Content.ReadAsStringAsync().Result;
                Logger.LogTransmission(trackingGUID, JsonConvert.SerializeObject(logMetadata, Formatting.Indented));

                return task.Result;
            });
        }

        protected Dictionary<string, string> extractHeaders(HttpHeaders h)
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