using System.Collections.Generic;
using IdentityServer4.Models;

namespace Tourney.Identity
{
    public class Resources
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource>()
            {
                new ApiResource("tournaments", "Tournaments")
                {
                    DisplayName = "Tournaments API",
                    UserClaims = new List<string>()
                    {
                        "role"
                    }
                }
            };
        }
    }
}