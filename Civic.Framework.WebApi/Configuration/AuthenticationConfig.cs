using Civic.Core.Configuration;

namespace Civic.Framework.WebApi.Configuration
{

	internal class AuthenticationConfig : NamedConfigurationElement
	{
        private static AuthenticationConfig _current;
        private static CivicSection _coreConfig;

        public AuthenticationConfig(INamedElement element)
        {
            if (element == null) element = new NamedConfigurationElement() { Name = SectionName };
            Children = element.Children;
            Attributes = element.Attributes;
            Name = element.Name;
        }

        /// <summary>
        /// The current configuration for the civic-api authentication system
        /// </summary>
        public static AuthenticationConfig Current
        {
            get
            {
                if (_current != null) return _current;
                if (_coreConfig == null) _coreConfig = CivicSection.Current;
                _current = new AuthenticationConfig(_coreConfig.Children.ContainsKey(SectionName) ? _coreConfig.Children[SectionName] : null);
                return _current;
            }
        }
        
        public bool UsernameHasDomain
	    {
	        get
	        {
	            if (Attributes.ContainsKey(Constants.CONFIG_USERNAMEHASDOMAIN_PROP))
	                return bool.Parse(Attributes[Constants.CONFIG_USERNAMEHASDOMAIN_PROP]);
	            return Constants.CONFIG_USERNAMEHASDOMAIN_DEFAULT;
            }
	        set { Attributes[Constants.CONFIG_USERNAMEHASDOMAIN_PROP] = value.ToString(); }
	    }

        /// <summary>
        /// The name of the configuration section.
        /// </summary>
        public static string SectionName
		{
			get { return Constants.AUTH_SECTION; }
		}
	}
}