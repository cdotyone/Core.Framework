using System;

namespace SAAS.Core.Framework
{
    [Flags]
    public enum EntityEventType
    {
        GetBefore = 1,
        GetAfter = 2,
        GetPagedBefore = 4,
        GetPagedAfter = 8,
        AddBefore = 16,
        AddAfter = 32,
        ModifyBefore = 64,
        ModifyAfter = 128,
        RemoveBefore = 256,
        RemoveAfter = 512
    }
}