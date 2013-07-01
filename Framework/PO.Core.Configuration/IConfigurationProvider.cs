using System;
using System.Configuration;

namespace PO.Core.Configuration
{
    /// <summary>
    /// Interface for configuration providers
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// The configuration for this provider
        /// </summary>
        CoreConfigurationProvider Configuration { get; set; }

        /// <summary>
        /// Gets the requested configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the section to retrieve</param>
        /// <param name="sectionType">The type of section that the configuration is ultimately expecting to hand back to the requestor</param>
        /// <returns>the requested configuration section</returns>
        ConfigurationSection GetSection(string sectionName, Type sectionType);

        /// <summary>
        /// Adds a configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the section to add</param>
        /// <param name="configurationSection">The configuration section to add</param>
        void Add(string sectionName, ConfigurationSection configurationSection);

        /// <summary>
        /// Removes a configuration section.
        /// </summary>
        /// <param name="sectionName">The name of the configuration section to remove</param>
        /// <param name="sectionType">The type of section that the configuration is ultimately expecting to remove</param>
        void Remove(string sectionName, Type sectionType);

        /// <summary>
        /// Updates a configuration section
        /// </summary>
        /// <param name="sectionName">The name of the section to update</param>
        /// <param name="configurationSection">The new values for the given section</param>
        void Update(string sectionName, ConfigurationSection configurationSection);

        /// <summary>
        /// This will return a connection string that is ready to be used by a database class
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">the name of the connection string</param>
        /// <returns>The unencrypted value of the connection string</returns>
        ConnectionStringSettings GetConnectionString(Type type, string name);

        /// <summary>
        /// This will save a connection string by name to a configuration provider
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="connectionSettings">The name, connection string, and provider name</param>
        void SetConnectionString(Type type, ConnectionStringSettings connectionSettings);

        /// <summary>
        /// Gets the value of an application setting for the specified type
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to pull the value from</param>
        /// <param name="name">The name of the value to retrieve</param>
        /// <returns>The value of the setting</returns>
        string GetAppSettingValue(Type type, string name);

        /// <summary>
        /// Sets the value of an application setting for the specified type
        /// </summary>
        /// <param name="type">The type that controls the bucket of name/value pairs to store the value into</param>
        /// <param name="name">The name of the value to set</param>
        /// <param name="value">The value of the setting</param>
        void SetAppSettingValue(Type type, string name, string value);
    }
}
