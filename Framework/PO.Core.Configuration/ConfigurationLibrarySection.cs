using System.Configuration;
using PO.Core.Configuration.Providers;

namespace PO.Core.Configuration
{
	public class ConfigurationLibrarySection : ConfigurationSection
	{
		/// <summary>
		/// The current configuration for the header footer
		/// </summary>
		public static ConfigurationLibrarySection Current
		{
			get
			{
				if (_coreConfig==null) _coreConfig = (ConfigurationLibrarySection)ConfigurationManager.GetSection(SectionName);
				if (_coreConfig == null || _coreConfig.Providers.Count == 0)
				{
					if(_coreConfig==null)
						_coreConfig = new ConfigurationLibrarySection
					    {
					        DefaultProvider = Constants.CONFIG_DEFAULTPROVIDER
					    };

					_coreConfig.Providers.Add(new ConfigurationProviderElement(new ConfigFileProvider()));
				}
				
				return _coreConfig;
			}
		}
		private static ConfigurationLibrarySection _coreConfig;

		/// <summary>
		/// Name of the configuration section for the header footer
		/// </summary>
		public static string SectionName
		{
			get { return Constants.CORE_CONFIG_SECTION; }
		}

		/// <summary>
		/// Gets or sets the typename for the skin for the header and footer
		/// </summary>
		[ConfigurationProperty(Constants.CONFIG_PROP_DEFAULTPROVIDER, IsRequired = false, DefaultValue = Constants.CONFIG_DEFAULTPROVIDER)]
		public string DefaultProvider
		{
			get { return (string)this[Constants.CONFIG_PROP_DEFAULTPROVIDER]; }
			set { this[Constants.CONFIG_PROP_DEFAULTPROVIDER] = value; }
		}

		/// <summary>
		/// Gets the collection of custom link type element collections.
		/// </summary>
		[ConfigurationProperty(Constants.CONFIG_PROP_PROVIDERS)]
		public NamedElementCollection<ConfigurationProviderElement> Providers
		{
			get
			{
				if (_providers==null) _providers = (NamedElementCollection<ConfigurationProviderElement>)base[Constants.CONFIG_PROP_PROVIDERS];
				if (_providers.Count == 0) _providers = null;
				if (_providers==null)
				{
					_providers = new NamedElementCollection<ConfigurationProviderElement>();
					_providers.Add(new ConfigurationProviderElement(new ConfigFileProvider()));
				}
				return _providers;
			}
		}
		private NamedElementCollection<ConfigurationProviderElement> _providers;

		/// <summary>
		/// Gets the collection of custom link type element collections.
		/// </summary>
		[ConfigurationProperty(Constants.CONFIG_PROP_REDIRECTIONS)]
		public NamedElementCollection<ConfigurationRedirection> Redirections
		{
			get
			{
				var redirections = (NamedElementCollection<ConfigurationRedirection>)base[Constants.CONFIG_PROP_REDIRECTIONS];
				if (redirections == null) redirections = new NamedElementCollection<ConfigurationRedirection>();
				return redirections;
			}
		}
	}
}
