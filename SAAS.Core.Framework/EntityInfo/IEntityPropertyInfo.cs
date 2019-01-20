using System;

namespace SAAS.Core.Framework
{
    public interface IEntityPropertyInfo
    {
        string Name { get; set; }

        bool IsNullable { get; set; }

        bool IsKey { get; set; }

        bool IsIdentity { get; set; }

        bool ForceUpperCase { get; set; }

        string Type { get; set; }

        string Default { get; set; }

        int? MaxLength { get; set; }

        int MaxQuery { get; set; }



        Delegate Set { get; set; }

        Delegate Get { get; set; }

        Type PropertyType { get; set; }
    }
}