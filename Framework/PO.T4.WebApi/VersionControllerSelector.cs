using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace PO.T4.WebApi
{
    // Implement DefaultHttpControllerSelector, so we can override SelectController
    public class VersionControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Regex _regex = new Regex(@"(https?://.*/api/)(v[0-9]*\.[0-9]*)(.*)");
        private readonly Dictionary<string, Type> _apiControllerTypes = new Dictionary<string, Type>();

        private Dictionary<string, Type> ApiControllerTypes
        {
            get { return _apiControllerTypes; }
        }

        public VersionControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }


        public void AddKnownRoutes(IEnumerable<EntityRoute> routes)
        {
            var routeList = routes.ToList();
            var types = routeList.Select(entityRoute => entityRoute.EntityType).ToArray();

            var metaDataRegistered = false;

            foreach (var entityRoute in routeList)
            {
                var key = entityRoute.Version.ToLower() + entityRoute.PluralName + ControllerSuffix;
                if(_apiControllerTypes.ContainsKey(key)) continue;
                _apiControllerTypes.Add(key, entityRoute.ControllerType);

                if (string.IsNullOrEmpty(entityRoute.PackageName))
                {
                    _configuration.Routes.MapHttpRoute(
                        name: "api_" + entityRoute.Version + "_" + entityRoute.PluralName,
                        routeTemplate: "api/v" + entityRoute.Version + "/" + entityRoute.PluralName + "/{id}",
                        defaults: new { id = RouteParameter.Optional, controller = entityRoute.PluralName, version = entityRoute.Version },
                        constraints: null
                        );
                }
                else
                {
                    _configuration.Routes.MapHttpRoute(
                        name: "api_" + entityRoute.PackageName + "_" + entityRoute.Version + "_" + entityRoute.PluralName,
                        routeTemplate: "api/" + entityRoute.PackageName + "/v" + entityRoute.Version + "/" + entityRoute.PluralName + "/{id}",
                        defaults: new { id = RouteParameter.Optional, controller = entityRoute.PluralName, version = entityRoute.Version },
                        constraints: null
                        );
                }

                _configuration.Formatters.XmlFormatter.SetSerializer(entityRoute.EntityType, new DataContractSerializer(entityRoute.EntityType, types));

                ODataV3EdmxFormatter.RegisterRoute(entityRoute);

                if (!metaDataRegistered)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(entityRoute.PackageName))
                        {
                            _configuration.Routes.MapHttpRoute(
                                name: "api_" + entityRoute.Version + "_$metadata",
                                routeTemplate: "api/v" + entityRoute.Version + "/$metadata",
                                defaults: new { id = RouteParameter.Optional, controller = entityRoute.PluralName, version = entityRoute.Version },
                                constraints: null
                                );
                            metaDataRegistered = true;
                        }
                        else
                        {
                            _configuration.Routes.MapHttpRoute(
                                name: "api_" + entityRoute.PackageName + "_" + entityRoute.Version + "_$metadata",
                                routeTemplate: "api/" + entityRoute.PackageName + "/v" + entityRoute.Version + "/$metadata",
                                defaults: new { id = RouteParameter.Optional, controller = entityRoute.PluralName, version = entityRoute.Version },
                                constraints: null
                                );
                            metaDataRegistered = true;
                        }
                    }
                    catch (ArgumentException) // bury duplicate metadata routes
                    {
                    }
                }
            }
        }

        public void AddUnknownRoutes(Assembly assembly)
        {
            var types =
                assembly.GetTypes().Where(t =>
                            !t.IsAbstract && t.Name.EndsWith(ControllerSuffix) &&
                            typeof (IHttpController).IsAssignableFrom(t));
                //.ToDictionary(t => t.FullName, t => t);

            foreach (var type in types)
            {
                if (_apiControllerTypes.ContainsKey(type.FullName)) continue;
                _apiControllerTypes.Add(type.FullName, type);
            }
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // override the Accept header if a known odata $format is passed on the querystring
            var query = request.RequestUri.ParseQueryString();
            var format = query.GetValues("$format");
            if (format != null)
            {
                if (string.Compare(format[0], "xml", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(format[0], "json", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (request.Headers.Contains("Accept")) request.Headers.Remove("Accept");
                    request.Headers.Add("Accept", "application/" + format[0].ToLower());
                }
            }

            // Fall back to using the default implementation if no version is specified
            return GetVersionedController(request) ?? base.SelectController(request);
        }

        private HttpControllerDescriptor GetVersionedController(HttpRequestMessage request)
        {
            var controllerName = GetControllerName(request);
            var data = request.GetRouteData();
            
            string version;
            if (data.Values.ContainsKey("version")) version = data.Values["version"].ToString();
            else version = GetRequestVersion(request);

            // Given null, the default implementation will get called
            if (version == null)
            {
                return null;
            }

            var type = GetControllerTypeByVersion(version, controllerName);

            if (type == null)
            {
                return null;
            }

            return new HttpControllerDescriptor(_configuration, controllerName, type);
        }

        private static string GetRequestVersion(HttpRequestMessage request)
        {
            
            //request.RequestUri = new Uri("somthing");

            // Find an accept header with a version parameter
            var version = request.Headers.Accept.Select(i =>
                {
                    var nameValueHeaderValue = i.Parameters.SingleOrDefault(s => s.Name == "version");
                    return nameValueHeaderValue != null ? nameValueHeaderValue.Value : null;
                }).ToList();
            return !version.Any() ? null : version.First();
        }

        private Type GetControllerTypeByVersion(string version, string controllerName)
        {
            // Stick to a convention, where a class name begins with the version number, this could be changed
            var versionToFind = string.Format("{0}", version.ToLower());
            var controllerNameToFind = string.Format("{0}{1}{2}", versionToFind, controllerName, ControllerSuffix);

            return ApiControllerTypes.Where(t => t.Key.ToLower().Contains(versionToFind.ToLower()) && t.Key.EndsWith(controllerNameToFind, StringComparison.OrdinalIgnoreCase)).Select(t => t.Value).FirstOrDefault();
        }
    }
}