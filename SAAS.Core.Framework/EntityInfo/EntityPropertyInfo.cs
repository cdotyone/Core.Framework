﻿using System;

namespace SAAS.Core.Framework
{
    /// <inheritdoc />
    public class EntityPropertyInfo : IEntityPropertyInfo
    {
        public string Name { get; set; }

        public bool IsNullable { get; set; }

        public bool IsKey { get; set; }

        public bool IsIdentity { get; set; }

        public bool ForceUpperCase { get; set; }

        public string Type { get; set; }
        
        public string Default { get; set; }

        public int? MaxLength { get; set; }

        public int MaxQuery { get; set; } = 100;

        public Delegate Set { get; set; }

        public Delegate Get { get; set; }

        public Type PropertyType { get; set; }
    }
}
