﻿namespace Civic.Framework.WebApi
{
    public interface IEntityOperation
    {
        IEntityIdentity Before { get; set; }

        IEntityIdentity After { get; set; }

        EntityOperationType Type { get; set; }

        void Commit();

        void Rollback();
    }
}