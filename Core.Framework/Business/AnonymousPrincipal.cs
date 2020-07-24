using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Core.Security;

namespace Core.Framework
{
    public class AnonymousPrincipal : ClaimsPrincipal
    {
        public AnonymousPrincipal()
        {

        }

        public AnonymousPrincipal(IEnumerable<Claim> claims)
        {
            _claims.AddRange(claims);
        }

        public AnonymousPrincipal(string type, string value)
        {
            AddClaim(type, value);
        }

        public AnonymousPrincipal(string value)
        {
            AddClaim(StandardClaimTypes.ROLE, value);
        }

        public AnonymousPrincipal(Claim claim)
        {
            AddClaim(claim);
        }

    private readonly List<ClaimsIdentity> _identities = new List<ClaimsIdentity>{new GenericIdentity("UNK")};
        public override IEnumerable<ClaimsIdentity> Identities
        {
            get { return _identities; }
        }

        public override IIdentity Identity
        {
            get
            {
                return _identities[0];
            }
        }

        private readonly List<Claim> _claims = new List<Claim>();
        public override IEnumerable<Claim> Claims
        {
            get { return _claims; }
        }

        void AddClaim(string type, string value)
        {
            _claims.Add(new Claim(type,value));
        }

        void AddClaim(Claim claim)
        {
            _claims.Add(claim);
        }
    }
}
