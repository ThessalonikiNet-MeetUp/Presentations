using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Account
{
	public class LoggedOutViewModel
	{
		public string PostLogoutRedirectUri { get; set; }
		public string ClientName { get; set; }
		public string SignOutIframeUrl { get; set; }
		public bool AutomaticRedirectAfterSignOut { get; set; }
	}

	[SecurityHeaders]
	[AllowAnonymous]
	public class LoggedOutModel : PageModel
	{
		private readonly IIdentityServerInteractionService _interactionService;

		public LoggedOutViewModel View { get; set; }

		public LoggedOutModel(IIdentityServerInteractionService interactionService)
		{
			_interactionService = interactionService;
		}

		public async Task OnGet(string logoutId)
		{
			// get context information (client name, post logout redirect URI and iframe for federated signout)
			var logout = await _interactionService.GetLogoutContextAsync(logoutId);

			View = new LoggedOutViewModel
			{
				AutomaticRedirectAfterSignOut = LogoutOptions.AutomaticRedirectAfterSignOut,
				PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
				ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
				SignOutIframeUrl = logout?.SignOutIFrameUrl
			};
		}
	}
}
