using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Security.Claims;
using CMS.Helper.StaticClass;

namespace IdentityServer
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentities() => new List<IdentityResource>()
        {
            //these are claim about user will be include in id_token
            new IdentityResources.OpenId(), //check the oAuth spec to know which claims will be in id_token if specify this scope
            new IdentityResources.Profile(),
            //scope can be request to get additional info about user
            new IdentityResource
            {
                Name = "User.Info",
                UserClaims =
                {
                    MyClaimType.Country,
                    MyClaimType.Role
                }
            }
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {            
            new ApiResource("MyApi"),
        };

        public static IEnumerable<ApiScope> GetScopes() => new List<ApiScope>
        {
            //user claims will be include in access token when request in openid middleware, identity resource is for id token
            new ApiScope("MyApi.Scope", new string[]
            {
                MyClaimType.Role //user claim should be specify here not in GetApis(access_token won't have tthis role claim)
            }),
        };
        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client()
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AllowedScopes = { "MyApi.Scope", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "User.Info" },

                RequireConsent = false,
                RedirectUris = { "https://localhost:7105/signin-oidc" }, //redirect auth url,
                AlwaysIncludeUserClaimsInIdToken = true, //require to add claims to id_token
            }
        };
    }
}
