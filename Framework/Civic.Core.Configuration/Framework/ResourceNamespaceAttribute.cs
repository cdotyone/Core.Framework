using System;

namespace Civic.Core.Configuration.Framework
{
	/// <summary>
	/// Allows control to redirect the ResourceManager to use a different namespace then the control is in
	/// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ResourceNamespaceAttribute : Attribute
    {
		public ResourceNamespaceAttribute(string nameSpace)
		{
			Namespace = nameSpace;
		}

        public string Namespace { get; private set; }
    }
}