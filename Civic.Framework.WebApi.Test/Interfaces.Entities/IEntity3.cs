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
using Civic.Framework.WebApi;
using Newtonsoft.Json;
using Civic.Framework.WebApi.Test.Interfaces;

namespace Civic.Framework.WebApi.Test.Interfaces
{
    public interface IEntity3 : IEntityIdentity
    {
    	 string SomeUID { get; set; }
    	 long SomeID { get; set; }
    	 DateTime Modified { get; set; }
    	 DateTime? OtherDate { get; set; }
    }
}

