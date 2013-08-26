using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Civic.Core.Configuration.Providers
{
    public class RegistryConfigProvider : IConfigurationProvider
    {
        private ConnectionStringsSection _connectionStrings;
        private AppSettingsSection _appSettings;
        private static readonly Dictionary<string, ConfigurationSection> _cache = new Dictionary<string, ConfigurationSection>();

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
            lock (_cache)
            {
                if (_cache.ContainsKey(sectionName)) return _cache[sectionName];
            }

            var config = ReadSectionFromRegistry(sectionName);
            if (string.IsNullOrEmpty(config)) return null;
            var section = Section.Deserialize(config, sectionType);

            lock (_cache)
            {
                _cache[sectionName] = section;                
            }

            return (section.Data ?? section) as ConfigurationSection;
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
            if (_connectionStrings == null)
            {
                var config = ReadSectionFromRegistry("connectionStrings");
                if (string.IsNullOrEmpty(config)) return null;
                var section = Section.Deserialize(config, typeof (ConnectionStringsSection));
                _connectionStrings = section.Data as ConnectionStringsSection;
            }

            if (_connectionStrings != null)
            {
                return _connectionStrings.ConnectionStrings[name];
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
            if (_connectionStrings == null)
            {
                var config = ReadSectionFromRegistry("appSettings");
                if (string.IsNullOrEmpty(config)) return null;
                var section = Section.Deserialize(config, typeof(AppSettingsSection));
                _appSettings = section.Data as AppSettingsSection;
            }

            if (_appSettings != null)
            {
                if (_appSettings.Settings[name] == null) return null;
                return _appSettings.Settings[name].Value;
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

        private string ReadSectionFromRegistry(string sectionName)
        {
            return ReadSectionFromRegistry(false, "UNIT", sectionName);
        }

        private string ReadSectionFromRegistry(bool user, string applicationCode, string sectionName)
        {
            var section = new XElement(sectionName);
            
            var rootKey = user ? Registry.CurrentUser : Registry.LocalMachine;
            var subkey = rootKey.OpenSubKey(string.Format(@"SOFTWARE\Deloitte\{0}\{1}", applicationCode, sectionName));
            if (subkey == null) return null;

            if (sectionName == "appSettings") RegistryKey2AppSettings(subkey, section);
            else RegistryKey2Element(subkey, section);

            return section.ToString();
        }

        private static void RegistryKey2AppSettings(RegistryKey key, XElement section)
        {
            foreach (var name in key.GetValueNames())
            {
                var value = key.GetValue(name).ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    var subelement = new XElement("add");
                    subelement.SetAttributeValue("key", name);
                    subelement.SetAttributeValue("value", value);
                    section.Add(subelement);
                }
            }
        }

        private static void RegistryKey2Element(RegistryKey key, XElement section)
        {
            foreach (var name in key.GetValueNames())
            {
                if (name == "_nn") continue;

                var value = key.GetValue(name).ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    section.SetAttributeValue(name, value);
                }
            }

            foreach (var subname in key.GetSubKeyNames())
            {
                var subkey = key.OpenSubKey(subname);
                var subelement = new XElement(subkey.GetValue("_nn") != null ? subkey.GetValue("_nn").ToString() : subname);
                section.Add(subelement);

                RegistryKey2Element(subkey, subelement);
            }
        }
    }
}
