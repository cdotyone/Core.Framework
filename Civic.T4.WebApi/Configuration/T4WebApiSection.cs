using System.Collections.Generic;
using System.Configuration;
using Civic.Core.Configuration;

namespace Civic.T4.WebApi.Configuration
{
    public class T4WebApiSection : Section
    {
        private static T4WebApiSection _current;
        private static bool _checked;
        private static Dictionary<string, int> _maxRowOverrides;
        private static Dictionary<string, bool> _forceUpperOverrides;

        /// <summary>
        /// The current configuration 
        /// </summary>
        public static T4WebApiSection Current
        {
            get
            {
                if (_current!=null) return _current;
                _current = ConfigurationFactory.ReadConfigSection<T4WebApiSection>(SectionName);
                _checked = true;
                return _current;
            }
        }

        /// <summary>
        /// Name of the configuration section for the header footer
        /// </summary>
        public static string SectionName { get { return Constants.CONFIG_SECTION; } }

        [ConfigurationProperty(Constants.CONFIG_FORCEUPPER, DefaultValue = false)]
        public bool ForceUpperCase { get; set; }

        [ConfigurationProperty(Constants.CONFIG_MAX, DefaultValue = 100)]
        public int Max { get; set; }

        public Dictionary<string, INamedElement> Entities
        {
            get
            {
                return Children;
            }
        }

        public static int GetMaxRows(string schema, string name)
        {
            var key = schema + "." + name;

            if (_maxRowOverrides == null) LoadMaxRowOverrides();
            return _maxRowOverrides != null && _maxRowOverrides.ContainsKey(key) ? _maxRowOverrides[key] : Current.Max;
        }

        public static void LoadMaxRowOverrides()
        {
            _maxRowOverrides = new Dictionary<string, int>();

            var config = Current;
            if (config != null)
            {
                var overrides = Current.Entities;
                foreach (var element in overrides)
                {
                    if (!element.Value.Attributes.ContainsKey(Constants.CONFIG_SCHEMA))
                        throw new ConfigurationErrorsException("schema is required for T4 entity configuration");
                    var ekey = element.Value.Attributes[Constants.CONFIG_SCHEMA] + "." + element.Key.ToLower();

                    if (!element.Value.Attributes.ContainsKey(Constants.CONFIG_MAX)) continue;

                    int max;
                    int.TryParse(element.Value.Attributes[Constants.CONFIG_MAX], out max);
                    _maxRowOverrides[ekey] = max;
                }
            }
        }

        public static string CheckUpperCase(string schema, string name, string field, string instring)
        {
            if (_forceUpperOverrides == null) LoadForceUpperOverrides();

            var key = schema + "." + name;
            var fkey = key + "." + field;

            if (_current == null && !_checked) _current = Current;

            var force = _current != null && _current.ForceUpperCase;
            if (_forceUpperOverrides != null)
            {
                if (_forceUpperOverrides.ContainsKey(fkey)) force = _forceUpperOverrides[fkey];
                else if (_forceUpperOverrides.ContainsKey(key)) force = _forceUpperOverrides[key];
            } 
            
            return force ? instring.ToUpperInvariant() : instring;
        }

        public static void LoadForceUpperOverrides()
        {
            _forceUpperOverrides = new Dictionary<string, bool>();

            var config = Current;
            if (config != null)
            {
                var overrides = Current.Entities;
                foreach (var element in overrides)
                {
                    if (!element.Value.Attributes.ContainsKey(Constants.CONFIG_SCHEMA))
                        throw new ConfigurationErrorsException("schema is required for T4 entity configuration");
                    var ekey = element.Value.Attributes[Constants.CONFIG_SCHEMA] + "." + element.Key.ToLower();
                    ekey = ekey.ToLowerInvariant();

                    if (element.Value.Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                    {
                        bool force;
                        if (!bool.TryParse(element.Value.Attributes[Constants.CONFIG_FORCEUPPER], out force)) continue;
                        _forceUpperOverrides[ekey] = force;
                    }

                    if (element.Value.Children != null && element.Value.Children.Count > 0)
                    {
                        foreach (var fieldConfig in element.Value.Children)
                        {
                            if (fieldConfig.Value.Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                            {
                                bool force;
                                if (!bool.TryParse(fieldConfig.Value.Attributes[Constants.CONFIG_FORCEUPPER], out force))
                                    continue;
                                ekey += "." + fieldConfig.Key.ToLowerInvariant();
                                _forceUpperOverrides[ekey] = force;
                            }
                        }
                    }

                    if (!element.Value.Attributes.ContainsKey(Constants.CONFIG_MAX)) continue;
                    int max;
                    int.TryParse(element.Value.Attributes[Constants.CONFIG_MAX], out max);
                    _maxRowOverrides[ekey] = max;
                }
            }
        }
    }
}
