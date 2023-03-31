using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Identity.Models;
using Identity.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Identity.Pages.Account.Register
{
	public class RegisterViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string? Email { get; set; }
		[Required]
		public string? Firstname { get; set; }
		[Required]
		public string? Lastname { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string? Password { get; set; }

		public string? ReturnUrl { get; set; }

	}

	[SecurityHeaders]
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IIdentityServerInteractionService _interaction;
		private readonly IEventService _events;
		private readonly IEmailService _emailService;

		public RegisterModel(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleInManager,
			IIdentityServerInteractionService interaction,
			IEventService events,
			IEmailService emailService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleInManager;
			_interaction = interaction;
			_events = events;
			_emailService = emailService;
		}

		[BindProperty]
		public RegisterViewModel Input { get; set; }

		public void OnGet(string returnUrl)
		{
			Input = new RegisterViewModel
			{
				ReturnUrl = returnUrl
			};
		}

		public async Task<IActionResult> OnPost(string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{
					UserName = Input.Email,
					Email = Input.Email,
					Firstname = Input.Firstname,
					Lastname = Input.Lastname,
				};

				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					//if (!_roleManager.RoleExistsAsync(Input.RoleName).GetAwaiter().GetResult())
					//{
					//    var userRole = new IdentityRole
					//    {
					//        Name = Input.RoleName,
					//        NormalizedName = Input.RoleName,

					//    };
					//    await _roleManager.CreateAsync(userRole);
					//}
					//await _userManager.AddToRoleAsync(user, Input.RoleName);


					await _userManager.AddClaimsAsync(user, new Claim[] {
						new Claim(JwtClaimTypes.Name,Input.Email),
						new Claim(JwtClaimTypes.Email,Input.Email),
						//new Claim(JwtClaimTypes.Role,Input.RoleName)
					});

					var loginresult = await _signInManager.PasswordSignInAsync(
						Input.Email, Input.Password, false, lockoutOnFailure: true);

					// Send confirm email address mail
					var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
					var confirmationLink = Url.Page("/Account/ConfirmEmail", null, new { token, email = user.Email }, Request.Scheme);

					await _emailService.SendAsync(user.Email, "Confirm Email", confirmationLink);

					if (loginresult.Succeeded)
					{
						var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
						var newUser = await _userManager.FindByNameAsync(Input.Email);
						await _events.RaiseAsync(new UserLoginSuccessEvent(newUser.UserName, newUser.Id, newUser.UserName, clientId: context?.Client.ClientId));

						return RedirectToPage("./AddPhoneNumber", new { returnUrl = Input.ReturnUrl });
					}

				}

			}
			return Page();
		}
	}
}
