using System.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PO.Core.Configuration
{
	/// <summary>
	/// Copy of SerializableConfigurationSection EntLib 4.1
	/// </summary>
	public class SerializableConfigurationSection : ConfigurationSection, IXmlSerializable
	{
		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			reader.Read();
			DeserializeSection(reader);
		}

		public void WriteXml(XmlWriter writer)
		{
			string data = SerializeSection(this, "SerializableConfigurationSection", ConfigurationSaveMode.Full);
			writer.WriteRaw(data);
		}
	}
}
