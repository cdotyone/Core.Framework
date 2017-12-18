﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Entity1")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF", ".NET 3.5")]
    public partial class ExampleEntity1Controller : ApiController
    {
        private static readonly ExampleService _service = new ExampleService();

        [Route("")]
        public QueryMetadata<Entity1Entity> Get()
        {
            ODataV3QueryOptions options = this.GetOptions();
            var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo", "entity1");
            var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
            string orderby = options.ProcessOrderByOptions();
            var result = _service.GetPagedEntity1(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
            return new QueryMetadata<Entity1Entity>(result, resultLimit);
        }

        [Route("{name}")]
        public QueryMetadata<Entity1Entity> Get(String name)
        {
            var result = new List<Entity1Entity> { _service.GetEntity1(name) };
            return new QueryMetadata<Entity1Entity>(result, 1);
        }

        [Route("")]
        public String Post([FromBody]Entity1Entity value)
        {
            _service.AddEntity1(value);
            return value.Name;
        }

        [Route("{name}")]
        public void Put(String name, [FromBody]Entity1Entity value)
        {
            value.Name = name;
            _service.ModifyEntity1(value);
        }

        [Route("{name}")]
        public void Delete(String name)
        {
            _service.RemoveEntity1(name);
        }
    }
}

