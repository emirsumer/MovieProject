using Microsoft.AspNetCore.Mvc;
using DataLayer;


namespace MovieProject.Controllers
{
	public class PricingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
