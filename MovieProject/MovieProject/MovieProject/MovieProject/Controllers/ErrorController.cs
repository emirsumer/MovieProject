using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
