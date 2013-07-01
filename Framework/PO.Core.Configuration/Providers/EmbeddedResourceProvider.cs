using System;
using System.Configuration;
using PO.Core.Configuration.Framework;

namespace PO.Core.Configuration.Providers
{
    /// <summary>
    /// Basic provider toload configurations from an embedded source.
    /// </summary>
	public class EmbeddedResourceProvider : IConfigurationProvider
	{
		/// <summary>
		/// The configuration for this provider
		/// </summary>
		public CoreConfigurationProvider Configuration { get; set; }

		/// <summary>
		/// Gets the requested configuration section.
		/// </summary>
		/// <param name="sectionName">The name of the section to retrieve</param>
		/// <param name="sectionType">The type of section that the configuration is ultimately expecting to hand back to the requestor</param>
		/// <returns>the requested configuration section</returns>
		public ConfigurationSection GetSection(string sectionName, Type sectionType)
		{
			var resourceName = GetNamespace(sectionType) + "." + sectionName;

			string config = ResourceHelper.GetStringResource(sectionType.Assembly, resourceName + ".config");
			if (string.IsNullOrEmpty(config))
			{
				config = ResourceHelper.GetStringResource(sectionType.Assembly, resourceName  + "." + ConfigurationFactory.Environment.ToLower() + ".config");
			}

			var section = Section.Deserialize(config, sectionType);
			return section;
		}

		/// <summary>
		/// Adds a configuration section.
		/// </summary>
		/// <param name="sectionName">The name of the section to add</param>
		/// <param name="configurationSection">The configuration section to add</param>
		public void Add(string sectionName, ConfigurationSection configurationSection)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes a configuration section.
		/// </summary>
		/// <param name="sectionName">The name of the configuration section to remove</param>
        /// <param name="sectionType">The type of section that the configuration is ultimately expecting to remove</param>
        public void Remove(string sectionName, Type sectionType)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Updates a configuration section
		/// </summary>
		/// <param name="sectionName">The name of the section to update</param>
		/// <param name="configurationSection">The new values for the given section</param>
		public void Update(string sectionName, ConfigurationSection configurationSection)
		{
			throw new NotImplementedException();
		}

	    /// <summary>
	    /// This will return a connection string that is ready to be used by a database class
	    /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">the name of the connection string</param>
	    /// <returns>The unencrypted value of the connection string</returns>
        public ConnectionStringSettings GetConnectionString(Type type, string name)
	    {
            var resourceName = GetNamespace(type) + ".connectionStrings";

            string config = ResourceHelper.GetStringResource(type.Assembly, resourceName + ".config");
            if (string.IsNullOrEmpty(config))
            {
                config = ResourceHelper.GetStringResource(type.Assembly, resourceName + "." + ConfigurationFactory.Environment.ToLower() + ".config");
            }

            var section = Section.Deserialize(config, typeof(ConnectionStringsSection));
	        var strings = section.Data as ConnectionStringsSection;
            if (strings != null)
            {
                return strings.ConnectionStrings[name];
            }

	        return null;
	    }

	    /// <summary>
	    /// This will save a connection string by name to a configuration provider
	    /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="connectionSettings">The name, connection string, and provider name</param>
        public void SetConnectionString(Type type, ConnectionStringSettings connectionSettings)
	    {
	        throw new NotImplementedException();
	    }

	    /// <summary>
	    /// Gets the value of an application setting for the specified type
	    /// </summary>
	    /// <param name="type">The type that controls the bucket of name/value pairs to pull the value from</param>
	    /// <param name="name">The name of the value to retrieve</param>
	    /// <returns>The value of the setting</returns>
	    public string GetAppSettingValue(Type type, string name)
	    {
            var resourceName = GetNamespace(type) + ".appSettings";

            string config = ResourceHelper.GetStringResource(type.Assembly, resourceName + ".config");
            if (string.IsNullOrEmpty(config))
            {
                config = ResourceHelper.GetStringResource(type.Assembly, resourceName + "." + ConfigurationFactory.Environment.ToLower() + ".config");
            }

            var section = Section.Deserialize(config, typeof(AppSettingsSection));
            var strings = section.Data as AppSettingsSection;
            if (strings != null)
            {
                if (strings.Settings[name] == null) return null;
                return strings.Settings[name].Value;
            }

            return null;
	    }

	    /// <summary>
	    /// Sets the value of an application setting for the specified type
	    /// </summary>
	    /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
	    /// <param name="name">The name of the value to set</param>
	    /// <param name="value">The value of the setting</param>
	    public void SetAppSettingValue(Type type, string name, string value)
	    {
	        throw new NotImplementedException();
	    }

	    /// <summary>
		/// determines resource namespace from type
		/// </summary>
		/// <param name="type">the type to as a reference point</param>
		/// <returns>the namespace</returns>
		public static string GetNamespace(Type type)
		{
			var nameSpace = type.FullName;

			// use the namespace override if it is available
			var attrs = type.GetCustomAttributes(typeof(ResourceNamespaceAttribute), true);
			if (attrs.Length > 0)
				return ((ResourceNamespaceAttribute)attrs[0]).Namespace;

			if (!string.IsNullOrEmpty(nameSpace))
			{
				var parts = nameSpace.Split('.');
				nameSpace = parts.Length > 1 ? string.Join(".", parts, 0, parts.Length - 1) : parts[0];
			}

			return nameSpace + ".Resources";
		}
	}
}
