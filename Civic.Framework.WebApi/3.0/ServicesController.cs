using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Civic.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Civic.Framework.WebApi
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        private readonly IEntityCreateFactory _factory;

        public ServicesController(IEntityCreateFactory factory)
        {
            _factory = factory;
        }

        [HttpPost]
        public IQueryMetadata PostBulk([FromBody]List<JObject> list)
        {
            var result = new List<object>();
            var identities = new Dictionary<string, string>();

            var context = new EntityRequestContext {Who = User as ClaimsPrincipal};

            ProcessRequests(list, identities, context, result, null);

            context.Commit();

            return new QueryMetadata<object>(result, 1);
        }

        private void ProcessRequests(List<JObject> list, Dictionary<string, string> identities, EntityRequestContext context, List<object> result, IEntityIdentity parent)
        {
            foreach (var obj in list)
            {
                string module = null;
                if (obj.Property("_schema") != null) { obj["_module"] = obj["_schema"].Value<string>();}
                if (obj.Property("_module") != null) module = obj["_module"].Value<string>();

                if (string.IsNullOrEmpty(module)) throw new Exception($"Missing _module on posted object {obj}");
                if (obj.Property("_action") == null) throw new Exception($"Missing _action on posted object {obj}");
                if (obj.Property("_entity") == null) throw new Exception($"Missing _entity name on posted object {obj}");

                var action = obj["_action"].Value<string>();
                if (action == "modify") action = "save";
                if (action == "add") action = "add";

                var entity = obj["_entity"].Value<string>();
                var identity = string.Empty;
                if (obj.Property("_identity") != null)
                {
                    identity = obj["_identity"].Value<string>();
                    obj.Remove("_identity");
                }

                List<JObject> children = null;
                if (obj.Property("_children") != null)
                {
                    children = obj["_children"].Value<List<JObject>>();
                    obj.Remove("_children");
                }

                var item = _factory.CreateNew(module, entity);

                if (parent != null)
                {
                    var info = item.GetInfo();
                    if (string.IsNullOrEmpty(info.RelatedKeyName))
                    {
                        throw new Exception($"Missing RelatedKeyName is not configured not sure what property to set on child object {obj}");
                    }

                    obj[info.RelatedKeyName] = parent.IdentityID;
                }

                var replace = (from prop in obj.Properties() where prop.Value.ToString().StartsWith("{{") select prop.Name).ToList();
                foreach (var prop in replace)
                {
                    var val = obj[prop].Value<string>().ToLower();
                    if (val.StartsWith("{{") && val.EndsWith("}}"))
                    {
                        val = val.Replace("{{", "");
                        val = val.Replace("}}", "");
                        if (identities.ContainsKey(val))
                        {
                            obj[prop] = new JValue(identities[val]);
                        }
                    }
                }

                try
                {
                    switch (action.ToLowerInvariant())
                    {
                        case "remove":
                            JsonConvert.PopulateObject(obj.ToString(), item);
                            item.Remove(context);

                            result.Add(item);
                            break;
                        case "save":

                            JsonConvert.PopulateObject(obj.ToString(), item);
                            item.Save(context);
                            if (!string.IsNullOrEmpty(identity)) identities[identity.ToLower()] = item.IdentityID;

                            result.Add(item);
                            break;
                    }

                    if (children != null)
                    {
                        ProcessRequests(children, identities, context, result, item);
                    }
                }
                catch (Exception ex)
                {
                    Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex);
                    context.Rollback();
                    throw;
                }
            }
        }
    }
}

