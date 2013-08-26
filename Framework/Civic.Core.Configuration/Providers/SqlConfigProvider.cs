using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

namespace Civic.Core.Configuration.Providers
{
    /// <summary>
    /// Basic provider toload configurations from an embedded source.
    /// </summary>
    public class SqlConfigProvider : IConfigurationProvider
	{
        private static readonly IEnumerable<string> NAMED_NODES = new []{"add","remove","clear"};
        private const string XML_HEAD = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        private const string BLANK_CONFIG = XML_HEAD + "<configuration/>";
        private XDocument _configuration;
        private ConnectionStringsSection _connectionStrings;
        private AppSettingsSection _appSettings;
        private static readonly Dictionary<string,ConfigurationSection> _cache = new Dictionary<string, ConfigurationSection>();
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
            Initialize();

            if (_configuration.Root != null)
            {
                var sectionElement = _configuration.Root.Elements(sectionName).FirstOrDefault();
                if (sectionElement != null)
                {
                    var section = Section.Deserialize(sectionElement.ToString(), sectionType);
                    lock (_cache)
                    {
                        _cache[sectionName] = section;
                    }
                    return (section.Data ?? section) as ConfigurationSection;
                }
            }

            return null;
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
            Initialize();
            return _connectionStrings.ConnectionStrings[name];
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
            Initialize();
            if (_appSettings.Settings[name] == null) return null;
            return _appSettings.Settings[name].Value;
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

        private void Initialize()
        {
            if (_configuration!=null) return;

            _configuration = ReadSectionsFromDb("DECO", "UNIT", "DEV");

            if (_configuration.Root != null)
            {
                var connectionStrings = _configuration.Root.Elements("connectionStrings").FirstOrDefault();
                if (connectionStrings != null)
                {
                    var connectionSection = Section.Deserialize(connectionStrings.ToString(), typeof(ConnectionStringsSection));
                    _connectionStrings = connectionSection.Data as ConnectionStringsSection;
                }
    
                var appSettings = _configuration.Root.Elements("appSettings").FirstOrDefault();
                if (appSettings != null)
                {
                    var appSettingsSection = Section.Deserialize(appSettings.ToString(), typeof(AppSettingsSection));
                    _appSettings = appSettingsSection.Data as AppSettingsSection;
                }
            }
        }

        private XDocument ReadSectionsFromDb(string connectionStringName, string applicationCode, string environmentCode)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "usp_ApplicationConfigGetByAppAndEnv"
                    };
                command.Parameters.Add("@applicationCode", SqlDbType.VarChar, 20).Value = applicationCode;
                command.Parameters.Add("@environmentCode", SqlDbType.VarChar, 10).Value = environmentCode;

                var xdoc = XDocument.Parse(BLANK_CONFIG);
                using (var reader = command.ExecuteReader())
                {
                    var columns = new Dictionary<string, int>();
                    while (reader.Read())
                    {
                        if (columns.Count == 0)
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                columns.Add(reader.GetName(i).ToLower(), i);
                            }
                        }

                        var settings = reader.IsDBNull(columns["settings"])
                                           ? string.Empty
                                           : reader.GetString(columns["settings"]);
                        MergeSectionIntoDocument(settings, xdoc);

                        var overridesettings = reader.IsDBNull(columns["overridesettings"]) ? string.Empty : reader.GetString(columns["overridesettings"]);
                        if (!string.IsNullOrEmpty(overridesettings))
                            MergeSectionIntoDocument(overridesettings, xdoc);
                    }
                }

                return xdoc;
            }
        }

        private static void MergeSectionIntoDocument(string settings, XDocument xdoc)
        {
            if (!string.IsNullOrEmpty(settings))
            {
                var xdoc2 = XDocument.Parse(settings);
                if (xdoc2.Root == null) return;
                if (xdoc2.Root.Name != "configuration")
                {
                    var xdoc3 = XDocument.Parse(BLANK_CONFIG);
                    if (xdoc3.Root != null) xdoc3.Root.Add(xdoc2.Elements().FirstOrDefault());
                    xdoc2 = xdoc3;
                }
                MergeXmlElements(xdoc2.Root, xdoc.Root);
            }
        }

        private static void MergeXmlElements(XElement sourceElement, XElement destination)
        {
            foreach (var attribute in sourceElement.Attributes())
            {
                destination.SetAttributeValue(attribute.Name, attribute.Value);
            }
            foreach (var node in sourceElement.Elements())
            {
                if (NAMED_NODES.Contains(node.Name.ToString()))
                {
                    if (node.Name == "add")
                    {
                        var addelement = destination.Elements("add").FirstOrDefault(x => x.Name == node.Name.ToString());
                        if (addelement == null) destination.Add(node);
                        else MergeXmlElements(node, addelement);
                    }  else 
                    if (node.Name == "delete")
                    {
                        var removeelement = destination.Elements("add").FirstOrDefault(x => x.Name == node.Name.ToString());
                        if (removeelement != null) removeelement.Remove();
                    } 
                    if (node.Name == "clear")
                    {
                        destination.RemoveAll();
                    } 
                }
                else
                {
                    var element = destination.Elements(node.Name).FirstOrDefault();
                    if (element == null) destination.Add(node);
                    else MergeXmlElements(node, element);
                }
            }
        }
	}
}
