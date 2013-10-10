using System;
using System.Reflection;

namespace Civic.T4.WebApi
{
	public class EntityRoute
	{
        public string PackageName { get; set; }

		public string EntityName { get; set; }

		public string PluralName { get; set; }

        public Type ControllerType { get; set; }

        public Type EntityType { get; set; }

        public string Version { get; set; }

        public Assembly EdmxAssembly { get; set; }
    
        public string EdmxResourceName { get; set; }

		public string[] CustomParameters { get; set; }
    }
}