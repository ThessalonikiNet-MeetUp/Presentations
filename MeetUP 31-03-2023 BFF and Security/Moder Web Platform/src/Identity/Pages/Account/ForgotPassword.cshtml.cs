using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Pages.Account.Register;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Identity.Pages.Account
{
	[AllowAnonymous]
	public class ForgotPasswordModel : PageModel
	{
		public class InputModel
		{
			[Required]
			[EmailAddress]
			public string Email { get; set; } = default!;
		}

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailService _emailService;

		public ForgotPasswordModel(
			UserManager<ApplicationUser> userManager,
			IEmailService emailService)
		{
			_userManager = userManager;
			_emailService = emailService;
		}

		[BindProperty]
		public InputModel Input { get; set; } = default!;

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(Input.Email);
				if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					// Don't reveal that the user does not exist or is not confirmed
					return RedirectToPage("./ForgotPasswordConfirmation");
				}

				// For more information on how to enable account confirmation and password reset please
				// visit https://go.microsoft.com/fwlink/?LinkID=532713
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
				var callbackUrl = Url.Page(
					"/Account/ResetPassword",
					pageHandler: null,
					values: new { token = encodedToken },
					protocol: Request.Scheme)!;

				await _emailService.SendAsync(user.Email, "Confirm Email", $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				return RedirectToPage("./ForgotPasswordConfirmation");
			}

			return Page();
		}

	}
}
