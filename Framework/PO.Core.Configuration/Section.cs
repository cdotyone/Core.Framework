using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Civic.Core.Configuration
{
    /// <summary>
    /// Represents a configuration section that can be serialized.
    /// </summary>
    public class Section : ConfigurationSection, INamedElement
    {
        private string _filename;
        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private readonly Dictionary<string, INamedElement> _children = new Dictionary<string, INamedElement>();
        private const string REMOVE_REQUIRES_NAME = "All <remove> tags must have a name property: {0}";
        private const string ADDS_REQUIRES_NAME = "All <add> tags must have a name property: {0}";
        private const string ADDS_REQUIRES_UNIQUE_NAME = "All <add> tags must have a unique name property: {0}";

        /// <summary>
        /// Returns the NULL for the XML schema for the configuration section.
        /// </summary>
        /// <returns>null</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Updates the configuration section from an XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader for the configuration source.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            DeserializeSection(reader);
        }

        /// <summary>
        /// Writes the configuration section to an XmlWriter.
        /// </summary>
        /// <param name="writer">The XmlWriter that writes to the configuration source.</param>
        public void WriteXml(XmlWriter writer)
        {
            var serialized = SerializeSection(this, "Section", ConfigurationSaveMode.Full);
            writer.WriteRaw(serialized);
        }

        public Dictionary<string, string> Attributes
        {
            get { return _attributes; }
        }

        public string Get(string name, string defaultValue = "")
        {
            if (_attributes.ContainsKey(name)) return _attributes[name];
            return defaultValue;
        }

        public Dictionary<string, INamedElement> Children
        {
            get { return _children; }
        }

        [ConfigurationProperty(Constants.CONFIG_PROP_PROVIDER, DefaultValue = Constants.CONFIG_PROP_DEFAULTPROVIDER)]
        public string Provider { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Name of assembly that contains strongly typed configuration section
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// Name of type for configuration section
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If Assembly and Type properties were provided than this should contain a strongly type section name
        /// If not than this should contain the XmlNode that represents the section.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Deserializes the configuration section in the configuration file.
        /// </summary>
        /// <param name="reader">The reader containing the XML for the section.</param>
        protected override void DeserializeSection(XmlReader reader)
        {
            DeserializeSection(reader, this);
        }

        public static Section Deserialize(string config, Type sectionType)
        {
            var reader = XmlReader.Create(new StringReader(config));
            var section = new Section();

            section.Assembly = sectionType.Assembly.FullName;
            section.Type = sectionType.FullName;

            DeserializeSection(reader, section);
            return section;
        }

        public static void DeserializeSection(XmlReader reader, Section section)
        {
            if (reader is IConfigErrorInfo && string.IsNullOrEmpty(section._filename)) section._filename = ((IConfigErrorInfo)reader).Filename;

            if (!reader.Read())
            {
                throw new ConfigurationErrorsException("Configuration reader expected to find an element", reader);
            }

            reader.MoveToContent();
            var doc = new XmlDocument();
            var sectionNode = doc.ReadNode(reader);
            if (sectionNode != null)
            {
                section.Data = sectionNode;

                if (sectionNode.Attributes != null)
                {
                    section.Name = sectionNode.Name;

                    var removeAttr = new List<XmlAttribute>();
                    foreach (XmlAttribute attribute in sectionNode.Attributes)
                    {
                        switch (attribute.Name.ToLower())
                        {
                            case "_type":
                                section.Type = attribute.Value;
                                removeAttr.Add(attribute);
                                break;
                            case "_assembly":
                                section.Assembly = attribute.Value;
                                removeAttr.Add(attribute);
                                break;
                            case "_provider":
                                section.Provider = attribute.Value;
                                removeAttr.Add(attribute);
                                break;
                            default:
                                if (attribute.Name.StartsWith("_")) continue;
                                break;
                        }
                        section._attributes.Add(attribute.Name, attribute.Value);
                    }
                    foreach (var xmlAttribute in removeAttr)
                    {
                        sectionNode.Attributes.Remove(xmlAttribute);
                    }

                    if (!string.IsNullOrEmpty(section.Assembly) && !string.IsNullOrEmpty(section.Type))
                        DeserializeTypedSection(sectionNode, section);
                    else
                    {
                        section.DeserializeElement(new XmlNodeReader(sectionNode), false);
                        section.Data = section;
                    }
                }
            }
        }

        /// <summary>
        /// Deserializes the configuration element in the configuration file.
        /// </summary>
        /// <param name="reader">The reader containing the XML for the section.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false. 
        /// Ignored in this implementation. </param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            DeserializeConfigElement(reader, this);
        }

        public static void DeserializeConfigElement(XmlReader reader, INamedElement section)
        {
            reader.MoveToContent();

            var doc = new XmlDocument();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var sectionNode = doc.ReadNode(reader);
                    if (sectionNode == null) continue;

                    AddSubElement(sectionNode, section);
                }
            }
        }

        internal static void AddSubElement(XmlNode sectionNode, INamedElement parent)
        {
            var name = sectionNode.Name;

            switch (sectionNode.Name)
            {
                case "add":
                    if (sectionNode.Attributes != null)
                        AddAsNamedElement(sectionNode, parent);
                    break;
                case "remove":
                    if (sectionNode.Attributes != null)
                        RemoveNamedElement(sectionNode, parent);
                    break;
                case "clear":
                    parent.Children.Clear(); // clear all named children
                    break;
                default:
                    AddChildElement(sectionNode, parent, name);
                    break;
            }
        }

        internal static void AddChildElement(XmlNode sectionNode, INamedElement parent, string name)
        {
            var element = new NamedConfigurationElement { Name = name };

            if (parent.Children.ContainsKey(name)) throw new ConfigurationErrorsException(string.Format(ADDS_REQUIRES_UNIQUE_NAME, sectionNode));
            parent.Children.Add(name, element);

            PopulateNode(sectionNode, element);
        }

        internal static void PopulateNode(XmlNode sectionNode, NamedConfigurationElement element)
        {
            if (sectionNode.Attributes != null)
            {
                var valueAttr = sectionNode.Attributes["value"];
                if (valueAttr != null) element.Value = valueAttr.Value;

                foreach (XmlAttribute attribute in sectionNode.Attributes)
                {
                    if (attribute.Value.StartsWith("##ENC##"))
                    {
                        var value = attribute.Value.Substring(7);
                        value = CryptoHelper.Decrypt(value);
                        attribute.Value = value;
                    }

                    element.Attributes.Add(attribute.Name, attribute.Value);
                }
            }

            if (sectionNode.ChildNodes.Count > 0)
            {
                foreach (XmlNode childElement in sectionNode.ChildNodes)
                {
                    if (childElement.NodeType != XmlNodeType.Element) continue;
                    AddSubElement(childElement, element);
                }
            }
        }

        private static void RemoveNamedElement(XmlNode sectionNode, INamedElement parent)
        {
            if (sectionNode.Attributes == null) throw new ConfigurationErrorsException(string.Format(REMOVE_REQUIRES_NAME, sectionNode));
            var nameAttr = sectionNode.Attributes["name"];
            if (nameAttr == null) throw new ConfigurationErrorsException(string.Format(REMOVE_REQUIRES_NAME, sectionNode));
            string name = nameAttr.Value;
            if (string.IsNullOrEmpty(name)) throw new ConfigurationErrorsException(string.Format(REMOVE_REQUIRES_NAME, sectionNode));

            if (parent.Children.ContainsKey(name))
                parent.Children.Remove(name);
        }

        private static void AddAsNamedElement(XmlNode sectionNode, INamedElement parent)
        {
            if (sectionNode.Attributes == null) throw new ConfigurationErrorsException(string.Format(ADDS_REQUIRES_NAME, sectionNode));
            var nameAttr = sectionNode.Attributes["name"];
            if (nameAttr == null) throw new ConfigurationErrorsException(string.Format(ADDS_REQUIRES_NAME, sectionNode));
            string name = nameAttr.Value;
            if (string.IsNullOrEmpty(name)) throw new ConfigurationErrorsException(string.Format(ADDS_REQUIRES_NAME, sectionNode));

            AddChildElement(sectionNode, parent, name);
        }

        /// <summary>
        /// Deserializes the data from the reader into a strongly type ConfigurationSection or class
        /// </summary>
        /// <param name="xmlNode">The XmlNode containing the serilized data.</param>
        /// <param name="section">Section that is being deserialized</param>
        private static void DeserializeTypedSection(XmlNode xmlNode, Section section)
        {
            var dynSection = DynamicInstance.CreateInstance(section.Assembly, section.Type);
            if (dynSection != null)
            {
                var reader = new XmlNodeReader(xmlNode);

                if (dynSection is ConfigurationSection)
                {
                    var deserializeSection = dynSection.GetType().GetMethod("DeserializeSection",
                                                                            BindingFlags.Instance | BindingFlags.NonPublic |
                                                                            BindingFlags.FlattenHierarchy);
                    deserializeSection.Invoke(dynSection, new object[] { reader });
                    section.Data = dynSection;
                }
                else
                {
                    reader.Read();
                    reader.MoveToContent();

                    var xRoot = new XmlRootAttribute(xmlNode.Name);

                    var serializer = new XmlSerializer(dynSection.GetType(), xRoot);
                    section.Data = serializer.Deserialize(reader);
                }

            }
        }

        //static public Section Create(ConfigurationSection section, string name)
        //{
        //    return Create(section, name, new Section());
        //}

        //static public Section Create(ConfigurationSection section, string name, Section newSection)
        //{

        //    var type = section.GetType();
        //    newSection.Assembly = type.Assembly.FullName;
        //    newSection.Type = type.FullName;
        //    newSection.Name = name;

        //    var xRoot = new XmlRootAttribute(name);

        //    var mem = new MemoryStream();
        //    var writer = XmlWriter.Create(mem);
        //    var serializer = new XmlSerializer(type, xRoot);
        //    serializer.Serialize(writer,section);
        //    mem.Seek(0, SeekOrigin.Begin);

        //    var xdoc = new XmlDocument();
        //    xdoc.Load(mem);
        //    newSection.Data = xdoc;

        //    return newSection;
        //}
    }
}
