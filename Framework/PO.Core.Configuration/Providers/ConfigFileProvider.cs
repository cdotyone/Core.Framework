using System;
using System.Configuration;

namespace Civic.Core.Configuration.Providers
{
    /// <summary>
    /// Basic provider for getting configurations from normal .config files.
    /// </summary>
    public class ConfigFileProvider : IConfigurationProvider
    {
        /// <summary>
        ///     The configuration for the provider
        /// </summary>
        public CoreConfigurationProvider Configuration { get; set; }

        /// <summary>
        ///     Gets the requested configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the section to retrieve</param>
        /// <param name="sectionType">The type of section that the configuration is ultimately expecting to hand back to the requestor</param>
        /// <returns>
        ///     the requested configuration section
        /// </returns>
        public ConfigurationSection GetSection(string sectionName, Type sectionType)
        {
            var configSection = (ConfigurationSection) ConfigurationManager.GetSection(sectionName);
            return configSection;
        }

        /// <summary>
        ///     Adds a configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the section to add</param>
        /// <param name="configurationSection">The configuration section to add</param>
        public void Add(string sectionName, ConfigurationSection configurationSection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes a configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the configuration section to remove</param>
        public void Remove(string sectionName, Type sectionType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates a configuration section
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
            return ConfigurationManager.ConnectionStrings[name];
        }

        /// <summary>
        /// This will save a connection string by name to a configuration provider
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="connectionSettings">The name, connection string, and provider name</param>
        public void SetConnectionString(Type type, ConnectionStringSettings connectionSettings)
        {
            ConfigurationManager.ConnectionStrings.Add(connectionSettings);
        }

        /// <summary>
        /// Gets the value of an application setting for the specified type
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to pull the value from</param>
        /// <param name="name">The name of the value to retrieve</param>
        /// <returns>The value of the setting</returns>
        public string GetAppSettingValue(Type type, string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        /// <summary>
        /// Sets the value of an application setting for the specified type
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">The name of the value to set</param>
        /// <param name="value">The value of the setting</param>
        public void SetAppSettingValue(Type type, string name, string value)
        {
            ConfigurationManager.AppSettings.Add(name, value);
        }
    }
}