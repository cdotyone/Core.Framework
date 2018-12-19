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
using System.Security.Claims;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Environment")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEnvironmentController : ApiController 
    {
    	private readonly IExample _service;
    
    	public ExampleEnvironmentController(IExample service)
        {
            service.Who = User as ClaimsPrincipal;
    		_service = service;
        }
    
    	[Route("")]
    	public QueryMetadata<EnvironmentEntity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","environment");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _service.GetPagedEnvironment(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<EnvironmentEntity>(result, resultLimit);
    	}
    
    	[Route("{id}")]
    	public QueryMetadata<EnvironmentEntity> Get( Int32 id )
    	{
    		var result = new List<EnvironmentEntity> { _service.GetEnvironment( id) };
    		return new QueryMetadata<EnvironmentEntity>(result, 1);
    	}
    
    	[Route("")]
    	public Int32 Post([FromBody]EnvironmentEntity value)
    	{
    		_service.AddEnvironment(value);
    		return value.ID;
    	}
    
    	[Route("{id}")]
    	public void Put(Int32 id, [FromBody]EnvironmentEntity value)
    	{
    		value.ID = id;
    		_service.ModifyEnvironment(value);
    	}
    
    	[Route("{id}")]
    	public void Delete( Int32 id )
    	{
    		_service.RemoveEnvironment( id );
    	}
    }
}

