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
using Core.Framework;
using Newtonsoft.Json;
using Core.Framework.Test.Interfaces;

namespace Core.Framework.Test.Interfaces
{
    
public interface IEntity3 : IEntityIdentity
{
	 string SomeUID { get; set; }
	 DateTime Modified { get; set; }
	 DateTime? OtherDate { get; set; }

}
}

