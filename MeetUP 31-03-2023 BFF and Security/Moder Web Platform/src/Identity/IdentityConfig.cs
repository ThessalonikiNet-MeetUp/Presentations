using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity
{
	public static class IdentityConfig
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
		new IdentityResource[]
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),

		};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
				{
					new ApiScope("core_domain_API", "The core domain API that holds all the business logic")
				};

		public static IEnumerable<Client> GetClients(Common.PublicWebApp publicWebApp)
		{
			return new Client[]
			{
				new Client {
					ClientId = Common.PublicWebApp.ClientId,
					ClientSecrets = { new Secret(publicWebApp.Secret.Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,
                
					// where to redirect to after login
					RedirectUris = { publicWebApp.RedirectURI },

					// where to redirect to after logout
					PostLogoutRedirectUris = { publicWebApp.PostLogoutRedirectUris },

					AllowOfflineAccess= true,

					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"core_domain_API"
					}
				}
			};
		}
	}
}
