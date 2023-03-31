using System.Text;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Identity.Pages.Account
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class ConfirmEmailModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> OnGet(string token, string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return RedirectToPage("Error");
			try
			{
				var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
				var result = await _userManager.ConfirmEmailAsync(user, token);
				ViewData["message"] = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
				return Page();
			}
			catch (Exception ex)
			{
				ViewData["message"] = ex.Message;
				return Page();
			}
		}
	}
}
