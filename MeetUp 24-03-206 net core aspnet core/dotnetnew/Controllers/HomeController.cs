using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication
{
    public class HomeController : Controller
    {
		[HttpGet("/")]
		public IActionResult Index() {
			return Content("Hello World from controller");
		}
    }
}