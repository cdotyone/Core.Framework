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
using Framework.Core;
using Newtonsoft.Json;
using Framework.Core.Test.Interfaces;

namespace Framework.Core.Test.Interfaces
{
    
public interface IEntity1 : IEntityIdentity
{
	 string Name { get; set; }
	 int EnvironmentID { get; set; }
	 DateTime Dte { get; set; }
	 DateTime? Dte2 { get; set; }
	 double Dble1 { get; set; }
	 double Dec1 { get; set; }

}
}

