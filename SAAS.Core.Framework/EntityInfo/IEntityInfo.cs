using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;

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


        bool? ForceUpperCase { get; set; }

        int? Max { get; set; }

        bool? CanView { get; set; }

        bool? CanAdd { get; set; }

        bool? CanModify { get; set; }

        bool? CanRemove { get; set; }

        bool? UseLocalTime { get; set; }


        /// <summary>
        /// The properties for the entity
        /// </summary>
        Dictionary<string, IEntityPropertyInfo> Properties { get; set; }


        #region Runtime Only

        /// <summary>
        /// True if the property setters and getters have been mapped
        /// </summary>
        [JsonIgnore]
        bool Mapped { get; set; }

        #endregion
    }
}
