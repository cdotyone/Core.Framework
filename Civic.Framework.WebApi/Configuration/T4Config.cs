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

        private static Dictionary<string, bool> _allowViewOverrides;
        private static Dictionary<string, bool> _allowAddOverrides;
        private static Dictionary<string, bool> _allowModifyOverrides;
        private static Dictionary<string, bool> _allowRemoveOverrides;

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

            if (Attributes.ContainsKey(Constants.CONFIG_CANVIEW))
                CanView = bool.Parse(Attributes[Constants.CONFIG_CANVIEW]);
            else CanView = true;
            if (Attributes.ContainsKey(Constants.CONFIG_CANADD))
                CanAdd = bool.Parse(Attributes[Constants.CONFIG_CANADD]);
            if (Attributes.ContainsKey(Constants.CONFIG_CANMODIFY))
                CanModify = bool.Parse(Attributes[Constants.CONFIG_CANMODIFY]);
            if (Attributes.ContainsKey(Constants.CONFIG_CANREMOVE))
                CanRemove = bool.Parse(Attributes[Constants.CONFIG_CANREMOVE]);


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

        [ConfigurationProperty(Constants.CONFIG_CANVIEW, DefaultValue = true)]
        public bool CanView { get; set; }

        [ConfigurationProperty(Constants.CONFIG_CANADD, DefaultValue = false)]
        public bool CanAdd { get; set; }

        [ConfigurationProperty(Constants.CONFIG_CANMODIFY, DefaultValue = false)]
        public bool CanModify { get; set; }

        [ConfigurationProperty(Constants.CONFIG_CANREMOVE, DefaultValue = false)]
        public bool CanRemove { get; set; }

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
            ekey = ekey.ToLowerInvariant();

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

        public static bool GetCanView(string schema, string name)
        {
            if (_allowViewOverrides == null) CacheConfig();

            var lschema = schema.ToLowerInvariant();
            var ekey = lschema + "." + name;
            ekey = ekey.ToLowerInvariant();
            if (_current == null && !_checked) _current = Current;

            var canAdd = _current == null ? true : _current.CanView;
            if (!canAdd) return false;
            if (_allowViewOverrides != null)
            {
                if (_allowViewOverrides.ContainsKey(lschema)) canAdd = _allowViewOverrides[lschema];
                else if (_allowViewOverrides.ContainsKey(ekey)) canAdd = _allowViewOverrides[ekey];
                else if (_allowViewOverrides.ContainsKey(schema)) canAdd = _allowViewOverrides[schema];
            }

            return canAdd;
        }

        public static bool GetCanAdd(string schema, string name)
        {
            if (_allowAddOverrides == null) CacheConfig();

            var lschema = schema.ToLowerInvariant();
            var ekey = lschema + "." + name;
            ekey = ekey.ToLowerInvariant();

            if (_current == null && !_checked) _current = Current;

            var canAdd = _current == null ? false : _current.CanAdd;
            if (canAdd) return true;
            if (_allowAddOverrides != null)
            {
                if(_allowAddOverrides.ContainsKey(lschema)) canAdd = _allowAddOverrides[lschema];
                else if (_allowAddOverrides.ContainsKey(ekey)) canAdd = _allowAddOverrides[ekey];
                else if (_allowAddOverrides.ContainsKey(schema)) canAdd = _allowAddOverrides[schema];
            }

            return canAdd;
        }

        public static bool GetCanModify(string schema, string name)
        {
            if (_allowModifyOverrides == null) CacheConfig();

            var lschema = schema.ToLowerInvariant();
            var ekey = lschema + "." + name;
            ekey = ekey.ToLowerInvariant();

            if (_current == null && !_checked) _current = Current;

            var canModify = _current == null ? false : _current.CanModify;
            if (canModify) return true;
            if (_allowModifyOverrides != null)
            {
                if (_allowModifyOverrides.ContainsKey(lschema)) canModify = _allowModifyOverrides[lschema];
                else if (_allowModifyOverrides.ContainsKey(ekey)) canModify = _allowModifyOverrides[ekey];
                else if (_allowModifyOverrides.ContainsKey(schema)) canModify = _allowModifyOverrides[schema];
            }

            return canModify;
        }

        public static bool GetCanRemove(string schema, string name)
        {
            if (_allowRemoveOverrides == null) CacheConfig();

            var lschema = schema.ToLowerInvariant();
            var ekey = lschema + "." + name;
            ekey = ekey.ToLowerInvariant();

            if (_current == null && !_checked) _current = Current;

            var canRemove = _current == null ? false : _current.CanRemove;
            if (canRemove) return true;
            if (_allowRemoveOverrides != null)
            {
                if (_allowRemoveOverrides.ContainsKey(lschema)) canRemove = _allowRemoveOverrides[lschema];
                else if (_allowRemoveOverrides.ContainsKey(ekey)) canRemove = _allowRemoveOverrides[ekey];
                else if (_allowRemoveOverrides.ContainsKey(schema)) canRemove = _allowRemoveOverrides[schema];
            }

            return canRemove;
        }


        public static void CacheConfig()
        {
            lock (_lock)
            {
                _maxRowOverrides = new Dictionary<string, int>();
                _forceUpperOverrides = new Dictionary<string, bool>();
                _allowAddOverrides = new Dictionary<string, bool>();
                _allowModifyOverrides = new Dictionary<string, bool>();
                _allowRemoveOverrides = new Dictionary<string, bool>();

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

                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_CANVIEW))
                        {
                            if (bool.TryParse(schema.Value.Attributes[Constants.CONFIG_CANVIEW], out force))
                                _allowViewOverrides[skey] = force;
                        }
                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_CANADD))
                        {
                            if (bool.TryParse(schema.Value.Attributes[Constants.CONFIG_CANADD], out force))
                                _allowAddOverrides[skey] = force;
                        }
                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_CANMODIFY))
                        {
                            if (bool.TryParse(schema.Value.Attributes[Constants.CONFIG_CANMODIFY], out force))
                                _allowModifyOverrides[skey] = force;
                        }
                        if (schema.Value.Attributes.ContainsKey(Constants.CONFIG_CANREMOVE))
                        {
                            if (bool.TryParse(schema.Value.Attributes[Constants.CONFIG_CANREMOVE], out force))
                                _allowRemoveOverrides[skey] = force;
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

                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_CANVIEW))
                                {
                                    if (bool.TryParse(element.Value.Attributes[Constants.CONFIG_CANVIEW], out force))
                                        _allowViewOverrides[ekey] = force;
                                }
                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_CANADD))
                                {
                                    if (bool.TryParse(element.Value.Attributes[Constants.CONFIG_CANADD], out force))
                                        _allowAddOverrides[ekey] = force;
                                }
                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_CANMODIFY))
                                {
                                    if (bool.TryParse(element.Value.Attributes[Constants.CONFIG_CANMODIFY], out force))
                                        _allowModifyOverrides[ekey] = force;
                                }
                                if (element.Value.Attributes.ContainsKey(Constants.CONFIG_CANREMOVE))
                                {
                                    if (bool.TryParse(element.Value.Attributes[Constants.CONFIG_CANREMOVE], out force))
                                        _allowRemoveOverrides[ekey] = force;
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

        public static string CheckUpperCase(string schema, string name, string field, string instring, bool allowEmpty = true)
        {
            if (string.IsNullOrEmpty(instring) && !allowEmpty) return allowEmpty ? instring : null;
            if (_forceUpperOverrides == null) CacheConfig();

            var ekey = schema + "." + name;
            ekey = ekey.ToLowerInvariant();
            var fkey = ekey + "." + field;
            fkey = fkey.ToLowerInvariant();

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
