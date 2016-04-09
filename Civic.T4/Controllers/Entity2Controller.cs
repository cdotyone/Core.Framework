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
using Civic.T4.Services;
using Civic.T4.Entities;
using Civic.T4.WebApi;
using Entity2Entity = Civic.T4.Entities.Entity2;

namespace Civic.T4.Controllers
{
    [RoutePrefix("api/example/1.0/Entity2")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF", ".NET 3.5")]
    public partial class Entity2Controller : ApiController
    {
        private static readonly ExampleService _service = new ExampleService();

        [Route("")]
        public QueryMetadata<Entity2Entity> Get()
        {
            ODataV3QueryOptions options = this.GetOptions();
            var maxrows = Civic.T4.WebApi.Configuration.T4Config.GetMaxRows("dbo", "entity2");
            var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
            string orderby = options.ProcessOrderByOptions();
            var result = _service.GetPagedEntity2(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
            return new QueryMetadata<Entity2Entity>(result, resultLimit);
        }

        [Route("{id}/{ff}")]
        public QueryMetadata<Entity2Entity> Get(Int32 id, String ff)
        {
            var result = new List<Entity2Entity> { _service.GetEntity2(id, ff) };
            return new QueryMetadata<Entity2Entity>(result, 1);
        }

        [Route("")]
        public String Post([FromBody]Entity2Entity value)
        {
            _service.AddEntity2(value);
            return value.ff;
        }

        [Route("{id}/{ff}")]
        public void Put(Int32 id, String ff, [FromBody]Entity2Entity value)
        {
            value.Id = id;
            value.ff = ff;
            _service.ModifyEntity2(value);
        }

        [Route("{id}/{ff}")]
        public void Delete(Int32 id, String ff)
        {
            _service.RemoveEntity2(id, ff);
        }
    }
}

