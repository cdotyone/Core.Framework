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
using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Services
{

    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEntity2
    {
        [OperationContract]
        List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy);

        [OperationContract]
        Entity2Entity GetEntity2(Int32 id, String ff);

        [OperationContract]
        void AddEntity2(Entity2Entity entity2);

        [OperationContract]
        void ModifyEntity2(Entity2Entity entity2);

        [OperationContract]
        void RemoveEntity2(Int32 id, String ff);

    }

}
