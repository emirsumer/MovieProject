using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
	public class UsersController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
            
            
                UserEdit userEdit = new UserEdit();
                TKullanici kullanici = new TKullanici();

                userEdit.kullanicilar = kullanici.LastUsers();

                return View(userEdit);

		}
    }
}
