using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace Civic.T4.WebApi.Security
{
    public class Token
    {
        const string MACHINE_KEY_PURPOSE = "CivicCommon:Username:{0}";
        const string ANONYMOUS = "<anonymous>";

        public string Seed { get; set; }

        public string Username { get; set; }

        public DateTime Expires { get; set; }

        [JsonIgnore]
        public bool IsExpired { get { return DateTime.UtcNow > Expires; } }

        public Dictionary<string, string> Properties
        {
            get { return _properties ?? (_properties = new Dictionary<string, string>()); }
            set { _properties = value; }
        }
        private Dictionary<string, string> _properties;


        public new string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool resetValues)
        {
            if (resetValues)
            {
                Seed = Guid.NewGuid().ToString().Replace("-", "");
                Expires = DateTime.UtcNow.AddMinutes(30);
            }
            return protect(JsonConvert.SerializeObject(this));
        }


        public static Token Parse(string value)
        {
            var json = unprotect(value);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<Token>(json);
        }

        private static string getMachineKeyPurpose(IPrincipal user)
        {
            return String.Format(MACHINE_KEY_PURPOSE,
                user.Identity.IsAuthenticated ? user.Identity.Name : ANONYMOUS);
        }

        private static string protect(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            var valueUtf = Encoding.UTF8.GetBytes(value);
            var purpose = getMachineKeyPurpose(Thread.CurrentPrincipal);
            var data = MachineKey.Protect(valueUtf, purpose);
            return HttpUtility.UrlEncode(Convert.ToBase64String(data) + "####");
        }

        private static string unprotect(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return null;
            var purpose = getMachineKeyPurpose(Thread.CurrentPrincipal);

            if (value.StartsWith("Bearer"))
            {
                value = value.Substring(7);
                value = HttpUtility.UrlDecode(value);
            }
            if (value != null && !value.EndsWith("####")) value = HttpUtility.UrlDecode(value);
            if (value != null) value = value.Substring(0, value.Length - 4);
            if (value != null) value = value.PadRight(value.Length + (4 - value.Length % 4) % 4, '=');

            if (value != null)
            {
                var bytes = Convert.FromBase64String(value);

                var valueUtf = MachineKey.Unprotect(bytes, purpose);

                return valueUtf != null ? Encoding.UTF8.GetString(valueUtf) : null;
            }

            return null;
        }
    }
}
