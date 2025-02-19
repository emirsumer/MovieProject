using DataLayer;

namespace Movie.Admin.Models
{
	public class UserEdit
	{
		public List<TblKullanici> kullanicilar { get; set; }
		public List<TblKisi> kisi { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime KayitZamani { get; set; } = DateTime.Now;

	}
}
