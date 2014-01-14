using System.Collections.Generic;
using Civic.Core.Configuration;

namespace Civic.T4.WebApi.Configuration
{
    public class T4WebApiSection : Section
    {
        /// <summary>
        /// The current configuration 
        /// </summary>
        public static T4WebApiSection Current
        {
            get { return ConfigurationFactory.ReadConfigSection<T4WebApiSection>(SectionName); }
        }

        /// <summary>
        /// Name of the configuration section for the header footer
        /// </summary>
        public static string SectionName { get { return Constants.CONFIG_SECTION; } }

        public static int GetMaxRows(string name)
        {
            if (_maxRowOverrides == null)
            {
                _maxRowOverrides = new Dictionary<string, int>();

                var overrides = Current.MaxRowsOverride;
                foreach (var element in overrides)
                {
                    int max;
                    int.TryParse(element.Value.Attributes["max"], out max);
                    _maxRowOverrides[element.Key.ToLower()] = max;
                }
            }
            return _maxRowOverrides.ContainsKey(name) ? _maxRowOverrides[name] : 100;
        }

        private static Dictionary<string, int> _maxRowOverrides;

        public Dictionary<string, INamedElement> MaxRowsOverride
        {
            get
            {
                return Children.ContainsKey(Constants.CONFIG_MAXROW)
                           ? Children[Constants.CONFIG_MAXROW].Children
                           : null;
            }
        }
    }
}
