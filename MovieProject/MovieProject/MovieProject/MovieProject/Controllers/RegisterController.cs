using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Layer.Business;


namespace MovieProject.Controllers
{
	public class RegisterController : Controller
	{
		public IActionResult Index()
		{
			return View();

		}

		[HttpPost]
		public IActionResult Register()
		{
			TKullanici kullanici = new();
			TblKullanici KullaniciBilgisi = new();
			string TxtUserName = Request.Form["TxtUserName"].ToString();
			string TxtSurname = Request.Form["TxtSurname"].ToString();
			string TxtMail = Request.Form["TxtMail"].ToString();
			string TxtGsm = Request.Form["TxtGsm "].ToString();
			string TxtPassword = Request.Form["TxtPassword "].ToString();

			KullaniciBilgisi.Aktif = true;
			KullaniciBilgisi.Sifre = TxtPassword;
			KullaniciBilgisi.KayitTarihi = DateTime.Now;
			KullaniciBilgisi.KullaniciAdi = TxtUserName;
			KullaniciBilgisi.SonGiris = DateTime.Now;
			KullaniciBilgisi.TblKisi = new();
			KullaniciBilgisi.TblKisi.Ad = TxtUserName;
			KullaniciBilgisi.TblKisi.Soyad = TxtSurname;
			KullaniciBilgisi.TblKisi.MailAdresi = TxtMail;
			KullaniciBilgisi.TblKisi.KisiGuid = Guid.NewGuid();

			string Mesaj;
			bool KayitEdildi = kullanici.Register(KullaniciBilgisi, out Mesaj);

			ViewBag.Basarili = KayitEdildi;
			ViewBag.Mesaj = Mesaj;
			return View("Login", ViewBag);
		}
	}
}
