using System;

namespace SAAS.Core.Framework
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RelatedAttribute : Attribute
    {
        /// <summary>
        /// The module of the related module (the parent entity)
        /// </summary>
        public string RelatedModule { get; set; }

        /// <summary>
        /// The name of the related module (the parent entity)
        /// </summary>
        public string RelatedEntity { get; set; }
    }
}