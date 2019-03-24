using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace SAAS.Core.Framework.OData
{
    public class ODataV3EdmxFormatter : XmlMediaTypeFormatter
    {
        private static readonly Dictionary<string, EntityRoute> _registeredRoutes = new Dictionary<string, EntityRoute>();

        private HttpRequestMessage _request;

        public ODataV3EdmxFormatter(HttpRequestMessage request)
        {
            _request = request;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, System.Net.TransportContext transportContext)
        {
            var entityType = type;
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                var enumer = enumerable.GetEnumerator();
                if (enumer.MoveNext()) entityType = enumer.Current.GetType();
            }
            if (_registeredRoutes.ContainsKey(entityType.FullName))
            {
                return Task.Factory.StartNew(() =>
                    {
                        var route = _registeredRoutes[entityType.FullName];
                        var assembly = route.EdmxAssembly;
                        using (var stream = new StreamReader(assembly.GetManifestResourceStream(route.EdmxResourceName)))
                        {
                            var outbuf = Encoding.UTF8.GetBytes(stream.ReadToEnd());
                            writeStream.Write(outbuf, 0, outbuf.Length);
                        };
                    });
            }
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        public static void RegisterRoute(EntityRoute route)
        {
            if (!_registeredRoutes.ContainsKey(route.EntityType.FullName))
                _registeredRoutes.Add(route.EntityType.FullName, route);
        }
    }
}