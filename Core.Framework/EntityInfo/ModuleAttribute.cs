using System;

namespace Core.Framework
{
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        /// Name of the module
        /// </summary>
        public string Name { get; set; }
    }
}
