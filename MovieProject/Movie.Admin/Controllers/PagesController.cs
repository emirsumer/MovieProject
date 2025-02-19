using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Movie.Admin.Controllers
{
	public class PagesController : Controller
	{
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPut]
		public IActionResult EditUser(UserEdit user, int id, TblKisi Kisi)
		{

			if (user == null)
			{
				return BadRequest();
			}

			TKullanici kullanici = new TKullanici();


			var updatedUser = new TblKullanici()
			{
				KullaniciAdi = user.Username,
				Sifre = user.Password,
				KayitTarihi = user.KayitZamani,

			};

			string message;
			bool success = kullanici.KullaniciEdit(id, updatedUser, Kisi, out message);
			var yesto = kullanici.GetUserMovieComments(id);
			var yesno = kullanici.GetUserSeriesComments(id);

			if (success)
			{
				return RedirectToAction("EditUser", "Pages");
			}
			else
			{
				return BadRequest();
			}

		}
	
		public IActionResult SignUp()
		{
			if (Request.Method == "POST")
			{
				TKullanici kullanici = new();
				string TxtSifre = Request.Form["TxtSifre"].ToString();
				string TxtKullaniciAdi = Request.Form["TxtKullaniciAdi"].ToString();
				string Message;
				TblKullanici Kullanici;
				string Mesaj;
				bool Success = kullanici.Login(TxtKullaniciAdi, TxtSifre, out Kullanici, out TblKisi kisi, out Message, out Mesaj);

				if (Success)
				{
					HttpContext.Session.SetString("Login", "1");
					HttpContext.Session.SetString("KullaniciId", Kullanici.KullaniciId.ToString());
					return Redirect("~/Home");
				}
			}
			return View();
		}
		public IActionResult SignIn()
		{
			return View();
		}
		public IActionResult Forgot()
		{
			return View();
		}
		public IActionResult Error()
		{
			return View();
		}

	}
}
