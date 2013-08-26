using System;
using System.Configuration;

namespace Civic.Core.Configuration
{
	public class ConfigurationRedirection : NamedConfigurationElement
	{
		private const string ASSEMBLY = "assembly";
		private const string VERSION = "version";
		private const string FROM = "from";

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
				if (string.IsNullOrEmpty(_assembly)) _assembly = (string)this[ASSEMBLY];
				return _assembly;
			}
			set { _assembly = value; }
		}
		private string _assembly;

		/// <summary>
		/// The version # or version range to redirect to a new section name
		/// 
		/// In the form: 1.0.0.0 
		/// Or: 1.0.0.0-2.0.0.0
		/// </summary>
		[ConfigurationProperty(VERSION, IsRequired = true)]
		public string Version
		{
			get
			{
				if (string.IsNullOrEmpty(_version)) _version = (string)this[VERSION];
				return _version;
			}
			set { _version = value; }
		}
		private string _version;

		/// <summary>
		/// The section name to redirect from.  The name of the section wired into the code
		/// </summary>
		[ConfigurationProperty(FROM, IsRequired = true)]
		public string From
		{
			get
			{
				if (string.IsNullOrEmpty(_from)) _from = (string)this[FROM];
				return _from;
			}
			set { _from = value; }
		}
		private string _from;

		/// <summary>
		/// Gets the beginning version # from the Version field
		/// </summary>
		public Version BeginVersion { 
			get
			{
				if(_beginVersion==null) InitVersion();
				return _beginVersion;
			} 
		}
		private Version _beginVersion;

		/// <summary>
		/// Gets the ending version # from the Version field
		/// </summary>
		public Version EndVersion
		{
			get
			{
				if (_endVersion == null) InitVersion();
				return _endVersion;
			}
		}
		private Version _endVersion;

		/// <summary>
		/// Checks to see if a verion is within range of this entry
		/// </summary>
		/// <param name="version">The version number to compare</param>
		/// <returns>true of within range</returns>
		public bool CheckIsInVersionRange(Version version)
		{
			var endVersion = EndVersion;
			return version >= BeginVersion && (endVersion == null || version <= endVersion);
		}

		/// <summary>
		/// Initializes the begin and end versions
		/// </summary>
		private void InitVersion()
		{
			if(string.IsNullOrEmpty(Version)) throw
				new ConfigurationErrorsException("redirection elements in the configuration redirections section requires a Version #");

			string[] pairs = Version.Split('-');
			_beginVersion = new Version(pairs[0]);

			if (pairs.Length > 1) _endVersion = new Version(pairs[1]);
			else _endVersion = null;
		}
	}
}