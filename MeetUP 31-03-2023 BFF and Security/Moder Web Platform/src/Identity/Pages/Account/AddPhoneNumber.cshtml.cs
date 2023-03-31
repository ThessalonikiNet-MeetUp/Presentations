using Common.Services;
using Identity.Models;
using Identity.Pages.Account.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Identity.Pages.Account
{
	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set;}		
	}
	[SecurityHeaders]
	public class AddPhoneNumberModel : PageModel
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ISmsService _smsService;

		public AddPhoneNumberModel(
			UserManager<ApplicationUser> userManager,
			ISmsService smsService)
		{
			_userManager = userManager;
			_smsService = smsService;
		}

		[BindProperty]
		public AddPhoneNumberViewModel Input { get; set; }

		public void OnGet()
        {			
        }

		public async Task<IActionResult> OnPost(string returnUrl)
		{
			if(ModelState.IsValid)
			{
				var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
				var token = await _userManager.GenerateChangePhoneNumberTokenAsync(applicationUser, Input.PhoneNumber);
				_smsService.SendAsync(Input.PhoneNumber, $"The code to verify your phone is: {token}");

				return RedirectToPage("./VerifyPhoneNumber", new { phoneNumber = Input.PhoneNumber, returnUrl });
			}
			return Page();
		}
    }
}
