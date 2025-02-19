using Microsoft.AspNetCore.Mvc;

namespace Movie.Admin.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
