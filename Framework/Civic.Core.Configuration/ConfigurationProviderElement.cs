using System.Configuration;
using Civic.Core.Configuration.Providers;

namespace Civic.Core.Configuration
{
	public class ConfigurationProviderElement : NamedConfigurationElement
	{
		private IConfigurationProvider _provider;
		private const string ASSEMBLY = "assembly";
		private const string TYPE = "type";

		/// <summary>
		/// The "assembly" name given of the provider.
		/// 
		/// In the form: assembly="Civic.Core.Configuration, Version=1.0.0.0, Culture=neutral"
		/// </summary>
		[ConfigurationProperty(ASSEMBLY, IsRequired = true)]
		public string AssemblyName
		{
			get
			{
				if (string.IsNullOrEmpty(_assembly)) _assembly = (string) this[ASSEMBLY];
				return _assembly;
			}
			set { _assembly = value; }
		}
		private string _assembly;

		/// <summary>
		/// The "type" name of the provider.
		/// 
		/// In the form of type="Civic.Core.Configuration.ConfigFileProvider"
		/// </summary>
		[ConfigurationProperty(TYPE, IsRequired = true)]
		public string TypeName
		{
			get
			{
				if (string.IsNullOrEmpty(_typeName)) _typeName = (string)this[TYPE];
				return _typeName;
			}
			set { _typeName = value; }
		}
		private string _typeName;

		/// <summary>
		/// Trys to dyanmically create the provider and then returns the provider.
		/// </summary>
		public IConfigurationProvider Provider
		{
			get {
				return _provider ?? (_provider = (IConfigurationProvider) DynamicInstance.CreateInstance(AssemblyName, TypeName));
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ConfigurationProviderElement()
		{
		}

		/// <summary>
		/// Creates a ConfigurationProviderElement from a IConfigurationProvider
		/// </summary>
		/// <param name="provider">the provider to create the configuration entry from</param>
		public ConfigurationProviderElement(IConfigurationProvider provider)
		{
			_provider = provider;

			var typeConfigFile = typeof(ConfigFileProvider);
			Name = typeConfigFile.Name;
			AssemblyName = typeConfigFile.Assembly.FullName;
			TypeName = typeConfigFile.FullName;

			if (Name.EndsWith("Provider")) Name = Name.Substring(0, Name.Length - 8);
		}
	}
}