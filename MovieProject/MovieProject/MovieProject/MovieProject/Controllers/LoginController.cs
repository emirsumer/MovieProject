using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
	public class LoginController : Controller
	{

		public IActionResult Index(TblKullanici Kullanici)
		{

			return View();
		}
		public IActionResult ForgotPassWord()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(TblKullanici Kullanici)
		{
			TKullanici KullaniciIslemleri = new();
			TblKullanici kullanici;
			TblKisi kisi;
			string token;
			string Mesaj = "";
			bool Basarili = KullaniciIslemleri.Login(Kullanici.KullaniciAdi, Kullanici.Sifre,
													out kullanici,
													out kisi,
													out Mesaj,
													out token);
			if (Basarili)
				return Ok(token);
			else
				return Unauthorized(Mesaj);
		}
	}
}
