using System;
using System.Collections.Generic;

namespace SAAS.Core.Framework
{
    public interface IEntityInfo
    {
        /// <summary>
        /// The module the entity belongs to
        /// </summary>
        string Module { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        string Entity { get; set; }

        /// <summary>
        /// The full name of the entity module.entity
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The module of the related module (the parent entity)
        /// </summary>
        string RelatedModule { get; set; }

        /// <summary>
        /// The name of the related module (the parent entity)
        /// </summary>
        string RelatedEntity { get; set; }

        /// <summary>
        /// The full name of the related entity RelatedModule.RelatedKeyName
        /// </summary>
        string RelatedKeyName { get; set; }

        /// <summary>
        /// The properties for the entity
        /// </summary>
        Dictionary<string, IEntityPropertyInfo> Properties { get; set; }


        #region Runtime Only

        /// <summary>
        /// True if the property setters and getters have been mapped
        /// </summary>
        bool Mapped { get; set; }

        #endregion
    }
}
