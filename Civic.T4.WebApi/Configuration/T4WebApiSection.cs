using System.Collections.Generic;
using Civic.Core.Configuration;

namespace Civic.T4.WebApi.Configuration
{
    public class T4WebApiSection : Section
    {
        /// <summary>
        /// The current configuration 
        /// </summary>
        public static T4WebApiSection Current
        {
            get { return ConfigurationFactory.ReadConfigSection<T4WebApiSection>(SectionName); }
        }

        /// <summary>
        /// Name of the configuration section for the header footer
        /// </summary>
        public static string SectionName { get { return Constants.CONFIG_SECTION; } }

        /// <summary>
        /// List of assemblies that the library should look for controllers in
        /// </summary>
        public Dictionary<string, INamedElement> Assemblies
        {
            get
            {
                return Children.ContainsKey(Constants.CONFIG_ASSEMBLIES) ? 
                    Children[Constants.CONFIG_ASSEMBLIES].Children : 
                    null;
            }
        }
    }
}
