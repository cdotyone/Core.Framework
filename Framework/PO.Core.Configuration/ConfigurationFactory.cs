using System;
using System.Collections.Generic;
using System.Configuration;

namespace PO.Core.Configuration
{
    public static class ConfigurationFactory
    {
    	private static readonly Dictionary<string, string> _redirections = new Dictionary<string, string>();
		private static readonly Dictionary<string,IConfigurationProvider> _providers = new Dictionary<string, IConfigurationProvider>();
		private static readonly Dictionary<string, Section> _cachedSections = new Dictionary<string, Section>();
		
    	static public string Environment
    	{
			get
			{
				var env = ConfigurationManager.AppSettings["CoreEnv"];
				return string.IsNullOrEmpty(env) ? "prod" : env;
			}
    	}

		#region ReadConfigValue

        /// <summary>
        /// Retrieves a simple name/value pair from the configuration equivalent to AppSettings
        /// </summary>
        /// <typeparam name="TType">Types are used to bucket settings and keep them from each other</typeparam>
        /// <param name="name">The name of the setting you want the value for</param>
        /// <returns>The value of the setting if found otherwise null</returns>
        public static string ReadConfigValue<TType>(string name)
		{
            return ReadConfigValue<TType>(name, string.Empty);
		}

        /// <summary>
        /// Retrieves a simple name/value pair from the configuration equivalent to AppSettings, or returns the given default
        /// </summary>
        /// <typeparam name="TType">Types are used to bucket settings and keep them from each other</typeparam>
        /// <param name="name">The name of the setting you want the value for</param>
        /// <param name="nullValue">The value to return if the setting is not found</param>
        /// <returns>The value of the setting if found otherwise null</returns>
        public static string ReadConfigValue<TType>(string name, string nullValue)
		{
            if (string.IsNullOrEmpty(name))
				throw new Exception(StringResources.GetString(Constants.SR.CONFIG_KEY_NULL));


			// TODO: ADD CACHING BACK IN
			//string configValue = CacheManager.ReadCache<string>(configKey, null, CacheStore.Application);
			//if (configValue == null)
			//{

            var configValue = Create().GetAppSettingValue(typeof(TType), name);
			if (configValue == null)
				configValue = nullValue;

				//CacheManager.WriteCache(configKey, configValue, CacheStore.Application);
			//}

			return configValue;
		}

        /// <summary>
        /// Retrieves a simple name/value pair from the configuration equivalent to AppSettings, or returns the given default
        /// </summary>
        /// <typeparam name="TType">Types are used to bucket settings and keep them from each other</typeparam>
        /// <param name="sourceName"></param>
        /// <param name="name">The name of the setting you want the value for</param>
        /// <param name="nullValue">The value to return if the setting is not found</param>
        /// <returns>The value of the setting if found otherwise null</returns>
        public static string ReadConfigValue<TType>(string sourceName, string name, string nullValue)
        {
            return ReadConfigValue(typeof(TType), sourceName, name, nullValue);
        }

        /// <summary>
        /// Retrieves a simple name/value pair from the configuration equivalent to AppSettings, or returns the given default
        /// </summary>
        /// <typeparam name="TType">Types are used to bucket settings and keep them from each other</typeparam>
        /// <param name="type"></param>
        /// <param name="sourceName"></param>
        /// <param name="name">The name of the setting you want the value for</param>
        /// <param name="nullValue">The value to return if the setting is not found</param>
        /// <returns>The value of the setting if found otherwise null</returns>
        public static string ReadConfigValue(Type type, string sourceName, string name, string nullValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(StringResources.GetString(Constants.SR.CONFIG_KEY_NULL));


            // TODO: ADD CACHING BACK IN
            //string configValue = CacheManager.ReadCache<string>(configKey, null, CacheStore.Application);
            //if (configValue == null)
            //{

            var configValue = Create(sourceName).GetAppSettingValue(type, name);
            if (configValue == null)
                configValue = nullValue;

            //CacheManager.WriteCache(configKey, configValue, CacheStore.Application);
            //}

            return configValue;
        }

		#endregion

		#region ReadConfigObject

		/// <summary>
		/// Retrieves a configuration object. This is just a deserialized configuration section.  It is strongly typed, but not based on a ConfigurationSection
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <typeparam name="TSection">The type to deserialize on, and the tyoe used to get the assembly for redirection information</typeparam>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>Returns the requested object, or null if it was unsuccessful</returns>
		public static TSection ReadConfigObject<TSection>(string sectionName)
		{
			var section = (Section)ReadConfigSection(typeof(TSection), string.Empty, sectionName, null);
			return (TSection)section.Data;
		}

		/// <summary>
		/// Retrieves a configuration object. This is just a deserialized configuration section.  It is strongly typed, but not based on a ConfigurationSection
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <typeparam name="TSection">The type to deserialize on, and the tyoe used to get the assembly for redirection information</typeparam>
		/// <param name="sectionName">The name of the section to get</param>
		/// <param name="nullValue">The default configuration to provide in the event no configuration is provided</param>
		/// <returns>Returns the requested object, or nullValue if it was unsuccessful</returns>
		public static TSection ReadConfigObject<TSection>(string sectionName, ConfigurationSection nullValue)
		{
			var section = (Section)ReadConfigSection(typeof(TSection), string.Empty, sectionName, nullValue);
			return (TSection)section.Data;
		}

		/// <summary>
		/// Retrieves a configuration object. This is just a deserialized configuration section.  It is strongly typed, but not based on a ConfigurationSection
		/// </summary>
		/// <typeparam name="TSection">The type to deserialize on, and the tyoe used to get the assembly for redirection information</typeparam>
		/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>Returns the requested object, or null if it was unsuccessful</returns>
		public static TSection ReadConfigObject<TSection>(string sourceName, string sectionName)
		{
			var section = (Section)ReadConfigSection(typeof(TSection), string.Empty, sectionName);
			return (TSection)section.Data;
		}

		#endregion ReadConfigObject

		#region ReadConfigSection

		#region typed configuration request

		/// <summary>
		/// Strongly Typed configuration request
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <typeparam name="TSection">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions.  Also the type of configurations section returned</typeparam>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>The configuration section requested of type TSection</returns>
		public static TSection ReadConfigSection<TSection>(string sectionName) where TSection : ConfigurationSection
		{
			var section = (Section)ReadConfigSection(typeof(TSection), string.Empty, sectionName, null);
			if (section == null) return null;
			return (TSection)section.Data;
		}

		/// <summary>
		/// Strongly Typed configuration request
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <typeparam name="TSection">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions.  Also the type of configurations section returned</typeparam>
		/// <param name="sectionName">The name of the section to get</param>
		/// <param name="nullValue">The default configuration to provide in the event no configuration is provided</param>
		/// <returns>The configuration section requested of type TSection</returns>
		public static TSection ReadConfigSection<TSection>(string sectionName, TSection nullValue) where TSection : ConfigurationSection
		{
			var section = (Section)ReadConfigSection(typeof(TSection), string.Empty, sectionName, nullValue);
            if (section == null) return nullValue;
            return (TSection)section.Data;
		}

		/// <summary>
		/// Strongly Typed configuration request
		/// </summary>
		/// <typeparam name="TSection">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions.  Also the type of configurations section returned</typeparam>
		/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>The configuration section requested of type TSection</returns>
		public static TSection ReadConfigSection<TSection>(string sourceName, string sectionName) where TSection : ConfigurationSection
		{
			var section = (Section)ReadConfigSection(typeof(TSection), sourceName, sectionName, null);
            if (section == null) throw new ConfigurationErrorsException(sectionName + " section could not be found from source " + sourceName);
			return (TSection)section.Data;
		}

    	/// <summary>
    	/// Strongly Typed configuration request
    	/// </summary>
    	/// <typeparam name="TSection">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions.  Also the type of configurations section returned</typeparam>
    	/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
    	/// <param name="sectionName">The name of the section to get</param>
    	/// <param name="nullValue">The default value sent back when configuration section not found</param>
    	/// <returns>The configuration section requested of type TSection</returns>
    	public static TSection ReadConfigSection<TSection>(string sourceName, string sectionName, TSection nullValue) where TSection : ConfigurationSection
		{
			var section = (Section)ReadConfigSection(typeof(TSection), sourceName, sectionName, null);
			if (section == null) return nullValue;
			return (TSection)section.Data;
		}

		#endregion typed configuration request

		#region non-typed configuration request

		/// <summary>
		/// Reads an non-typed configuration section 
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <param name="type">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>The configuration section requested.  This returns a Deloitte.Core.Configuration.Section</returns>
		public static ConfigurationSection ReadConfigSection(Type type, string sectionName)
        {
			return ReadConfigSection(type, string.Empty, sectionName, null);
        }

		/// <summary>
		/// Reads an non-typed configuration section 
		/// 
		/// The default configuration provider is used.
		/// </summary>
		/// <param name="type">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <param name="nullValue">The default configuration to provide in the event no configuration is provided</param>
		/// <returns>The configuration section requested.  This returns a Deloitte.Core.Configuration.Section</returns>
		public static ConfigurationSection ReadConfigSection(Type type, string sectionName, ConfigurationSection nullValue)
        {
			return ReadConfigSection(type, string.Empty, sectionName, nullValue);
        }

		/// <summary>
		/// Reads an non-typed configuration section 
		/// </summary>
		/// <param name="type">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions</param>
		/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <returns>The configuration section requested.  This returns a Deloitte.Core.Configuration.Section</returns>
		public static ConfigurationSection ReadConfigSection(Type type, string sourceName, string sectionName)
        {
			return ReadConfigSection(type, sourceName, sectionName, null);
        }

		#endregion non-typed configuration request

		/// <summary>
		/// Primary method to retrieve a configuration section for a request.  This is called all of the overloaded methods.
		/// </summary>
		/// <param name="type">The type used to get the assembly that the configuration is bound to.  This is used for redirection of configuration sections between assembly versions</param>
		/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
		/// <param name="sectionName">The name of the section to get</param>
		/// <param name="nullValue">The default configuration to provide in the event no configuration is provided</param>
		/// <returns>The configuration section.  This is normally a Deloitte.Core.Configuration.Section</returns>
		public static ConfigurationSection ReadConfigSection(Type type, string sourceName, string sectionName, ConfigurationSection nullValue)
        {
			// make sure we have the parameters we need.
			if (type == null)
				throw new ArgumentNullException("type");
			if (string.IsNullOrEmpty(sectionName))
				throw new ArgumentNullException("sectionName");

			// make sure we have a valid configuration, this should always be true, but do it anyways
			var config = ConfigurationLibrarySection.Current;
			if (config == null)
				throw new ConfigurationErrorsException("The " + ConfigurationLibrarySection.SectionName + " could not be found, and default configuration provider list was given.");

			// variables for checking for redirection
			var asmName = type.Assembly.GetName();
			var version = asmName.Version;
			var assemblyName = asmName.Name;
			var asmCacheKey = sectionName + "," + assemblyName + ", " + version;
		
			// has the redirection information been figured out yet.
			if (_redirections.ContainsKey(asmCacheKey)) sectionName = _redirections[asmCacheKey];  // it has so just use it
			else
			{
				// move through redirection list and see if we find a match
				foreach (var redirection in config.Redirections)
				{
					if(string.Compare(sectionName, redirection.From,StringComparison.InvariantCultureIgnoreCase)!=0) continue;
					if(string.Compare(assemblyName, redirection.AssemblyName, StringComparison.InvariantCultureIgnoreCase) != 0) continue;

					if (redirection.CheckIsInVersionRange(version))
					{
						sectionName = redirection.Name;
						break;
					}
				}

				// save the redirection information for this section
				lock (_redirections)
				{
					if (!_redirections.ContainsKey(asmCacheKey))
						_redirections.Add(asmCacheKey, sectionName);
				}
			}

			// look for section in cache
		    var cacheKey = sourceName + "_" + sectionName;
            if (_cachedSections.ContainsKey(cacheKey)) return _cachedSections[cacheKey];

			// return the requested section
			var configSection = Create(sourceName).GetSection(sectionName, type) ?? nullValue;

			// not there so try to load
			// only cache if provider does not have its own caching
			if (configSection != null)
			{
				var retSection = configSection as Section;
				if (retSection == null) retSection = new Section { Name = sectionName, Data = configSection };

				// lock cache and add new section
				lock (_cachedSections)
				{
                    if (!_cachedSections.ContainsKey(cacheKey))
					{
                        _cachedSections.Add(cacheKey, retSection);
					}
				}

				return retSection;
			}

			return null;
        }

		#endregion ReadConfigSection

        #region Connection Strings
        
        /// <summary>
        /// This will return a connection string that is ready to be used by a database class
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">the name of the connection string</param>
        /// <returns>The unencrypted value of the connection string</returns>
        public static ConnectionStringSettings GetConnectionString(Type type, string name)
        {
            return Create().GetConnectionString(type, name);
        }

        /// <summary>
        /// This will return a connection string that is ready to be used by a database class
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">the name of the connection string</param>
        /// <param name="sourceName">This is the name of the configuration provider to force</param>
        /// <returns>The unencrypted value of the connection string</returns>
        public static ConnectionStringSettings GetConnectionString(Type type, string name, string sourceName)
        {
            return Create(sourceName).GetConnectionString(type, name);
        }

        #endregion Connection Strings

        #region Create Providers

        /// <summary>
		/// Gets the default provider
		/// 
		/// Really should be used internally only unless you really know what your doing.
		/// Going directly to providers bypasses caching and other safeguards
		/// </summary>
		/// <returns>The default provider</returns>
		public static IConfigurationProvider Create()
		{
			return Create(string.Empty);
		}

		/// <summary>
		/// Gets a specific provider
		/// 
		/// Really should be used internally only unless you really know what your doing.
		/// Going directly to providers bypasses caching and other safeguards
		/// </summary>
		/// <param name="sourceName">The configuration name given to the provider in the configuration</param>
		/// <returns>The request provider</returns>
    	public static IConfigurationProvider Create(string sourceName)
        {
        	var config = ConfigurationLibrarySection.Current;
			if (config==null)
				throw new ConfigurationErrorsException("The " + ConfigurationLibrarySection.SectionName + " could not be found, and default configuration provider list was given.");

			if (string.IsNullOrEmpty(sourceName)) sourceName = config.DefaultProvider;

			if (_providers.ContainsKey(sourceName)) return _providers[sourceName];

			var providerConfig = config.Providers.Get(sourceName);
			if (providerConfig==null)
				throw new ConfigurationErrorsException("The " + sourceName + " could not be found in the current list of providers in the " + ConfigurationLibrarySection.SectionName + " section.");

        	var provider = providerConfig.Provider;
			if(provider!=null)
			{
				lock(_providers)
				{
					if(!_providers.ContainsKey(sourceName))
						_providers.Add(sourceName,provider);
				}
				return provider;
			}

			return null;
        }
	
		#endregion Create Providers
	}
}

