using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Identity.Pages.Account
{
	public class VerifyPhoneNumberViewModel
	{
		[Required]
		[StringLength(6)]
		[Display(Name = "Code")]
		public string? Code { get; set; }

		public string PhoneNumber { get; set; }
	}
	[SecurityHeaders]
	public class VerifyPhoneNumberModel : PageModel
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public VerifyPhoneNumberModel(UserManager<ApplicationUser> userManager)
		{
			_userManager= userManager;
		}

		[BindProperty]
		public VerifyPhoneNumberViewModel Input { get; set; }

		public void OnGet(string phoneNumber)
        {
			Input = new VerifyPhoneNumberViewModel {
				PhoneNumber = phoneNumber,
			};
        }

		public async Task<ActionResult> OnPost(string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
				var result = await _userManager.ChangePhoneNumberAsync(applicationUser, Input.PhoneNumber, Input.Code);
				if (result.Succeeded)
				{
					if (Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else if (string.IsNullOrEmpty(returnUrl))
					{
						return Redirect("~/");
					}
					else
					{
						throw new Exception("invalid return URL");
					}
				}
				else
				{
					ModelState.AddModelError("", "Failed to verify phone");
				}
			}
			return Page();
		}
    }
}
