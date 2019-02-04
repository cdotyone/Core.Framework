using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Civic.Core.Logging;
using SAAS.Core.Framework.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SAAS.Core.Framework
{
    //[RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        private readonly IEntityCreateFactory _factory;

        public ServicesController(IEntityCreateFactory factory)
        {
            _factory = factory;
        }

        //[Route("{module}/{version}/{entity}")]
        [ActionName("DefaultGetPaged")]
        public IQueryMetadata GetPaged(string module, string version, string entity)
        {
            var item = _factory.CreateNew(module, entity);
            var info = EntityInfoManager.GetInfo(item);

            ODataV3QueryOptions options = this.GetOptions();
            var max = EntityInfoManager.GetMaxRows(info);
            var resultLimit = options.Top < max && options.Top > 0 ? options.Top : max;
            string orderBy = options.ProcessOrderByOptions();

            var context = new EntityRequestContext { Who = User as ClaimsPrincipal };

            var facade = _factory.CreateFacade(item);
            var result = facade.GetPaged(context, options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderBy);
            return new QueryMetadata<object>(result, resultLimit);
        }

        //[Route("{module}/{version}/{entity}/{key}")]
        [ActionName("DefaultGet")]
        public IQueryMetadata Get(string module, string version, string entity, string key)
        {
            var item = _factory.CreateNew(module, entity);

            var context = new EntityRequestContext { Who = User as ClaimsPrincipal };

            var facade = _factory.CreateFacade(item);
            var result = new List<object> { facade.Get(context, key) };
            return new QueryMetadata<object>(result, 1);
        }

        //[Route("{module}/{version}/{entity}/{key}")]
        [ActionName("DefaultDelete")]
        public void Remove(string module, string version, string entity, string key)
        {
            var item = _factory.CreateNew(module, entity);

            var context = new EntityRequestContext { Who = User as ClaimsPrincipal };

            var facade = _factory.CreateFacade(item);
            facade.Remove(context, item);
        }


        //[Route("{module}/{version}/{entity}")]
        [ActionName("DefaultPost")]
        public QueryMetadata<IEntityIdentity> Post(string module, string version, string entity, [FromBody]JObject value)
        {
            var item = _factory.CreateNew(module, entity);

            JsonConvert.PopulateObject(value.ToString(),item);
            var context = new EntityRequestContext { Who = User as ClaimsPrincipal };

            var facade = _factory.CreateFacade(item);
            facade.Save(context, item);

            return new QueryMetadata<IEntityIdentity>(new [] {item}, 1);
        }

        //[Route("{module}/{version}/{entity}/{key}")]
        [ActionName("DefaultPut")]
        public void Put(string module, string version, string entity, string key, [FromBody]JObject value)
        {
            var item = _factory.CreateNew(module, entity);

            item._key = key;
            JsonConvert.PopulateObject(value.ToString(), item);
            var context = new EntityRequestContext { Who = User as ClaimsPrincipal };

            var facade = _factory.CreateFacade(item);
            facade.Save(context, item);
        }

        [ActionName("DefaultPostBulk")]
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
                    var info = EntityInfoManager.GetInfo(item);
                    if (string.IsNullOrEmpty(info.RelatedKeyName))
                    {
                        throw new Exception($"Missing RelatedKeyName is not configured not sure what property to set on child object {obj}");
                    }

                    obj[info.RelatedKeyName] = parent._key;
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
                    var facade = _factory.CreateFacade(item);

                    switch (action.ToLowerInvariant())
                    {
                        case "remove":
                            JsonConvert.PopulateObject(obj.ToString(), item);
                            facade.Remove(context, item);

                            result.Add(item);
                            break;
                        case "save":

                            JsonConvert.PopulateObject(obj.ToString(), item);
                            facade.Save(context, item);
                            if (!string.IsNullOrEmpty(identity)) identities[identity.ToLower()] = item._key;

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

