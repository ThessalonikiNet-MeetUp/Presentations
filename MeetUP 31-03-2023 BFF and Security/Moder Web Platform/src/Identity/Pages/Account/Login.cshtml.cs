using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Account
{
	public class InputModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public bool RememberLogin { get; set; }

		public string ReturnUrl { get; set; }

		public string Button { get; set; }
	}

	public class LoginOptions
	{
		public static bool AllowLocalLogin = true;
		public static bool AllowRememberLogin = true;
		public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
		public static string InvalidCredentialsErrorMessage = "Invalid username or password";
	}

	public class LoginViewModel
	{
		public bool AllowRememberLogin { get; set; } = true;
		public bool EnableLocalLogin { get; set; } = true;

		public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
		public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

		public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
		public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

		public class ExternalProvider
		{
			public string DisplayName { get; set; }
			public string AuthenticationScheme { get; set; }
		}
	}

	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IIdentityServerInteractionService _interaction;
		private readonly IEventService _events;
		private readonly IAuthenticationSchemeProvider _schemeProvider;
		private readonly IIdentityProviderStore _identityProviderStore;

		public LoginViewModel View { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public LoginModel(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IIdentityServerInteractionService interaction,
			IEventService events,
			IAuthenticationSchemeProvider schemeProvider,
			IIdentityProviderStore identityProviderStore)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_interaction = interaction;
			_events = events;
			_schemeProvider = schemeProvider;
			_identityProviderStore = identityProviderStore;
		}

		public async Task<IActionResult> OnGet(string returnUrl)
		{
			await BuildModelAsync(returnUrl);

			if (View.IsExternalLoginOnly)
			{
				// we only have one option for logging in and it's an external provider
				return RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl });
			}

			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			// check if we are in the context of an authorization request
			var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

			// the user clicked the "cancel" button
			if (Input.Button != "login")
			{
				if (context != null)
				{
					// if the user cancels, send a result back into IdentityServer as if they 
					// denied the consent (even if this client does not require consent).
					// this will send back an access denied OIDC error response to the client.
					await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

					// we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
					if (context.IsNativeClient())
					{
						// The client is native, so this change in how to
						// return the response is for better UX for the end user.
						return this.LoadingPage(Input.ReturnUrl);
					}

					return Redirect(Input.ReturnUrl);
				}
				else
				{
					// since we don't have a valid context, then we just go back to the home page
					return Redirect("~/");
				}
			}

			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberLogin, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					var user = await _userManager.FindByNameAsync(Input.Username);
					await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

					if (context != null)
					{
						if (context.IsNativeClient())
						{
							// The client is native, so this change in how to
							// return the response is for better UX for the end user.
							return this.LoadingPage(Input.ReturnUrl);
						}

						// we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
						return Redirect(Input.ReturnUrl);
					}

					// request for a local page
					if (Url.IsLocalUrl(Input.ReturnUrl))
					{
						return Redirect(Input.ReturnUrl);
					}
					else if (string.IsNullOrEmpty(Input.ReturnUrl))
					{
						return Redirect("~/");
					}
					else
					{
						// user might have clicked on a malicious link - should be logged
						throw new Exception("invalid return URL");
					}
				}

				await _events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials", clientId: context?.Client.ClientId));
				ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
			}

			// something went wrong, show form with error
			await BuildModelAsync(Input.ReturnUrl);
			return Page();
		}

		private async Task BuildModelAsync(string returnUrl)
		{
			Input = new InputModel
			{
				ReturnUrl = returnUrl
			};

			var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
			if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
			{
				var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;

				// this is meant to short circuit the UI and only trigger the one external IdP
				View = new LoginViewModel
				{
					EnableLocalLogin = local,
				};

				Input.Username = context?.LoginHint;

				if (!local)
				{
					View.ExternalProviders = new[] { new LoginViewModel.ExternalProvider { AuthenticationScheme = context.IdP } };
				}

				return;
			}

			var schemes = await _schemeProvider.GetAllSchemesAsync();

			var providers = schemes
				.Where(x => x.DisplayName != null)
				.Select(x => new LoginViewModel.ExternalProvider
				{
					DisplayName = x.DisplayName ?? x.Name,
					AuthenticationScheme = x.Name
				}).ToList();

			var dyanmicSchemes = (await _identityProviderStore.GetAllSchemeNamesAsync())
				.Where(x => x.Enabled)
				.Select(x => new LoginViewModel.ExternalProvider
				{
					AuthenticationScheme = x.Scheme,
					DisplayName = x.DisplayName
				});
			providers.AddRange(dyanmicSchemes);


			var allowLocal = true;
			var client = context?.Client;
			if (client != null)
			{
				allowLocal = client.EnableLocalLogin;
				if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
				{
					providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
				}
			}

			View = new LoginViewModel
			{
				AllowRememberLogin = LoginOptions.AllowRememberLogin,
				EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin,
				ExternalProviders = providers.ToArray()
			};
		}
	}
}
