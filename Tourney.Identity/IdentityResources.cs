using System.Collections.Generic;
using IdentityServer4.Models;

namespace Tourney.Identity
{
    public class Identities
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}