using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
    public class AddItemController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }
    }
}
