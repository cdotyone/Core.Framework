using System.Collections.Generic;
using System.Configuration;
using Civic.Core.Configuration;

namespace Civic.Framework.WebApi.Configuration
{
    public class T4Config : Section
    {
        private static bool _checked;
        private static Dictionary<string, int> _maxRowOverrides;
        private static Dictionary<string, bool> _forceUpperOverrides;
        private static object _lock = new object();

        public T4Config(INamedElement element)
        {
            if (element == null) element = new NamedConfigurationElement() { Name = SectionName };
            Children = element.Children;
            Attributes = element.Attributes;
            Name = element.Name;

            if (Attributes.ContainsKey(Constants.CONFIG_USE_LOCALTIME_PROP))
                UseLocalTime = bool.Parse(Attributes[Constants.CONFIG_USE_LOCALTIME_PROP]);
            if (Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                ForceUpperCase = bool.Parse(Attributes[Constants.CONFIG_FORCEUPPER]);
            if (Attributes.ContainsKey(Constants.CONFIG_MAX))
                Max = int.Parse(Attributes[Constants.CONFIG_MAX]);
        }

        /// <summary>
        /// The current configuration for caching module
        /// </summary>
        public static T4Config Current
        {
            get
            {
                if (_current != null) return _current;
                if (_coreConfig == null) _coreConfig = CivicSection.Current;

                _current = new T4Config(_coreConfig.Children.ContainsKey(SectionName) ? _coreConfig.Children[SectionName] : null);


                _checked = true;

                return _current;
            }
        }
        private static CivicSection _coreConfig;
        private static T4Config _current;

        /// <summary>
        /// Name of the configuration section for the header footer
        /// </summary>
        public static string SectionName { get { return Constants.CONFIG_SECTION; } }

        [ConfigurationProperty(Constants.CONFIG_FORCEUPPER, DefaultValue = false)]
        public bool ForceUpperCase { get; set; }

        [ConfigurationProperty(Constants.CONFIG_MAX, DefaultValue = 100)]
        public int Max { get; set; }

        [ConfigurationProperty(Constants.CONFIG_USE_LOCALTIME_PROP, DefaultValue = Constants.CONFIG_USE_LOCALTIME_DEFAULT)]
        public bool UseLocalTime { get; set; }

        public Dictionary<string, INamedElement> Entities
        {
            get
            {
                return Children;
            }
        }

        public static int GetMaxRows(string schema, string name)
        {
            if (_maxRowOverrides == null) CacheConfig();

            var ekey = schema + "." + name;

            if (_current == null && !_checked) _current = Current;

            var max = _current == null ? 100 : _current.Max;
            if (max <= 0) max = 100;
            if (_maxRowOverrides != null)
            {
                if (_maxRowOverrides.ContainsKey(ekey)) max = _maxRowOverrides[ekey];
                else if (_maxRowOverrides.ContainsKey(schema)) max = _maxRowOverrides[schema];
            }

            return max;
        }

        public static void CacheConfig()
        {
            lock (_lock)
            {
                _maxRowOverrides = new Dictionary<string, int>();
                _forceUpperOverrides = new Dictionary<string, bool>();

                var config = Current;
                if (config != null)
                {
                    var overrides = Current.Entities;
                    foreach (var schema in overrides)
                    {
                        var skey = schema.Key.ToLower();
                        int max;
                        bool force;

                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_MAX))
                        {
                            if (int.TryParse(schema.Value.Attributes[Constants.CONFIG_MAX], out max))
                                _maxRowOverrides[skey] = max;
                        }
                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                        {
                            if (bool.TryParse(schema.Value.Attributes[Constants.CONFIG_FORCEUPPER], out force))
                                _forceUpperOverrides[skey] = force;
                        }


                        if (schema.Value.Children != null)
                        {
                            foreach (var element in schema.Value.Children)
                            {
                                var ekey = skey + "." + element.Key.ToLower();
                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_MAX))
                                {
                                    if (int.TryParse(element.Value.Attributes[Constants.CONFIG_MAX], out max))
                                        _maxRowOverrides[ekey] = max;
                                }
                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                                {
                                    if (bool.TryParse(element.Value.Attributes[Constants.CONFIG_FORCEUPPER], out force))
                                        _forceUpperOverrides[ekey] = force;
                                }

                                if (element.Value.Children != null)
                                {
                                    foreach (var field in element.Value.Children)
                                    {
                                        var fkey = ekey + "." + field.Key.ToLower();
                                        if (field.Value.Attributes.ContainsKey(Constants.CONFIG_FORCEUPPER))
                                        {
                                            if (bool.TryParse(field.Value.Attributes[Constants.CONFIG_FORCEUPPER], out force))
                                                _forceUpperOverrides[fkey] = force;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string CheckUpperCase(string schema, string name, string field, string instring)
        {
            if (string.IsNullOrEmpty(instring)) return instring;
            if (_forceUpperOverrides == null) CacheConfig();

            var ekey = schema + "." + name;
            var fkey = ekey + "." + field;

            if (_current == null && !_checked) _current = Current;

            var force = _current != null && _current.ForceUpperCase;
            if (_forceUpperOverrides != null)
            {
                if (_forceUpperOverrides.ContainsKey(fkey)) force = _forceUpperOverrides[fkey];
                else if (_forceUpperOverrides.ContainsKey(ekey)) force = _forceUpperOverrides[ekey];
                else if (_forceUpperOverrides.ContainsKey(schema)) force = _forceUpperOverrides[schema];
            } 
            
            return force ? instring.ToUpperInvariant() : instring;
        }
    }
}
