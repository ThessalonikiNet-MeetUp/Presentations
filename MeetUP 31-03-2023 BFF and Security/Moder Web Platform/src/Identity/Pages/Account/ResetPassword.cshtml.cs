using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.Pages.Account
{

	[AllowAnonymous]
	public class ResetPasswordModel : PageModel
	{
		public class InputModel
		{
			[Required]
			[EmailAddress]
			public string Email { get; set; } = default!;

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			public string Password { get; set; } = default!;

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string? ConfirmPassword { get; set; }

			[Required]
			public string Token { get; set; } = default!;
		}

		private readonly UserManager<ApplicationUser> _userManager;

		public ResetPasswordModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		[BindProperty]
		public InputModel Input { get; set; } = default!;

		public IActionResult OnGet(string? token = null)
		{
			if (token == null)
			{
				return BadRequest("A code must be supplied for password reset.");
			}
			else
			{
				Input = new InputModel
				{
					Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token))
				};
				return Page();
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userManager.FindByEmailAsync(Input.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			var result = await _userManager.ResetPasswordAsync(user, Input.Token, Input.Password);
			if (result.Succeeded)
			{
				return RedirectToPage("./ResetPasswordConfirmation");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return Page();
		}
	}
}
