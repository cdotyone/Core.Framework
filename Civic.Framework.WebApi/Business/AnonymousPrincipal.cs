﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Civic.Framework.WebApi
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

        public AnonymousPrincipal(Claim claim)
        {
            AddClaim(claim);
        }

    private readonly List<ClaimsIdentity> _identities = new List<ClaimsIdentity>{new GenericIdentity("UNK")};
        public override IEnumerable<ClaimsIdentity> Identities
        {
            get { return _identities; }
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
