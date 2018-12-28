﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is to supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Interfaces;
using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Entity3")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEntity3Controller : ApiController 
    {
    	private readonly IExampleFacade _facade;
    
    	public ExampleEntity3Controller(IExampleFacade facade)
        {
    		_facade = facade;
        }
    
    	[Route("")]
    	public QueryMetadata<Entity3Entity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","entity3");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _facade.GetPagedEntity3(User as ClaimsPrincipal, options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<Entity3Entity>(result, resultLimit);
    	}
    
    	[Route("{someUID}")]
    	public QueryMetadata<Entity3Entity> Get( String someUID )
    	{
    		var result = new List<Entity3Entity> { _facade.GetEntity3(User as ClaimsPrincipal,  someUID) };
    		return new QueryMetadata<Entity3Entity>(result, 1);
    	}
    
    	[Route("")]
    	public QueryMetadata<Entity3Entity> Post([FromBody]Entity3Entity value)
    	{
    		_facade.SaveEntity3(User as ClaimsPrincipal, value);
    		var result = new List<Entity3Entity> { value };
    		return new QueryMetadata<Entity3Entity>(result, 1);
    	}
    
    	[Route("{someUID}")]
    	public void Put(String someUID, [FromBody]Entity3Entity value)
    	{
    		_facade.SaveEntity3(User as ClaimsPrincipal, value);
    	}
    
    	[Route("{someUID}")]
    	public void Delete( String someUID )
    	{
    		_facade.RemoveEntity3(User as ClaimsPrincipal,  someUID );
    	}
    }
}

