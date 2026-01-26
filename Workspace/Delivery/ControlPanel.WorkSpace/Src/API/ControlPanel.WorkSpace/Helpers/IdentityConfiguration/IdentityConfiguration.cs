

namespace UserManagement.Helpers.IdentityConfiguration
{
    public class IdentityConfiguration
    {


        public static IEnumerable<Duende.IdentityServer.Models.ApiResource> ApiResources => new Duende.IdentityServer.Models.ApiResource[]
           {
            // Create a new API resource named "UserManagementServer" with specified settings.
            new Duende.IdentityServer.Models.ApiResource("UserManagementServer")
                 {   // The API allows requests with the "write" and "read" scopes.
                     Scopes = new List<string> { "ReadAndWrite" },
                     // It requires the "supersecret" API secret for authentication.
                     ApiSecrets = new List<Duende.IdentityServer.Models.Secret> { new Duende.IdentityServer.Models.Secret("supersecret".Sha256()) },
                     // The issued token will contain user claims: role, name, and email.
                     UserClaims = new[] {
                         JwtClaimTypes.Role ,
                         JwtClaimTypes.Name ,
                         JwtClaimTypes.Email,
                         "FirstName"
                     },
                 }
           };

        public static IEnumerable<Duende.IdentityServer.Models.IdentityResource> IdentityResources => new Duende.IdentityServer.Models.IdentityResource[]
        {
             new IdentityResources.OpenId(),
             new IdentityResources.Profile(),
             new IdentityResource("Role", "User role(s)", new List<string> { "Role" }),
             new IdentityResource("FirstName", "User's first name", new List<string> { "FirstName" }),

        };

        public static IEnumerable<Duende.IdentityServer.Models.Client> Clients => new Duende.IdentityServer.Models.Client[]
        {
        new Duende.IdentityServer.Models.Client
        {
            ClientId = "client",
            ClientName = "Client With UserName And Password",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Duende.IdentityServer.Models.Secret("secret".Sha256()) },
            AllowedScopes = { "ReadAndWrite" },
            AlwaysIncludeUserClaimsInIdToken = true,
            AccessTokenLifetime = 60*60*24, // 1 Day

        }
        };
        // list of all scope 
        public static IEnumerable<Duende.IdentityServer.Models.ApiScope> ApiScopes => new Duende.IdentityServer.Models.ApiScope[]
              {    new Duende.IdentityServer.Models.ApiScope("ReadAndWrite"),
              };
    }
}