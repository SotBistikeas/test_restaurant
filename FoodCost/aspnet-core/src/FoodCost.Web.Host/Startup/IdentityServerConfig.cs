using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCost.Web.Host.Startup
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
        {
            new ApiResource("default-api", "Default (all) API")
        };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone()
        };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials.Union(GrantTypes.ResourceOwnerPassword).ToList(),
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 60*60*24*10, //10 meres
                    IdentityTokenLifetime = 60*60*24*30, //30 meres
                    RefreshTokenUsage = TokenUsage.ReUse,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowedScopes = {"default-api"},
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }
    }
}
