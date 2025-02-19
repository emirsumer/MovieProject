using DataLayer;
using Jose.keys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Layer.Business
{
	public class TKullanici : DbProcess, IDatabase
	{
		public TKullanici()
		{
			this.Context = new DbMovieEntities();
			DbConnect();
		}
		public void DbConnect()
		{
			this.ConnectDb();
		}

		public void DbDisconnect()
		{
			this.DisconnectDb();
		}

		public bool Login(string KullaniciAdi, string Sifre, out TblKullanici kullanici, out TblKisi kisi, out string Mesaj, out string token)
		{
			bool result = false;
			Mesaj = string.Empty;
			kullanici = null;
			kisi = null;
			token = string.Empty;
			try
			{
				var KullaniciData = (from data in Context.TblKullanici
									 where data.KullaniciAdi == KullaniciAdi &&
									 data.Sifre == Sifre
									 select data).AsNoTracking().FirstOrDefault();
				if (KullaniciData != null)
				{
					kullanici = KullaniciData;
					kisi = KullaniciData.TblKisi;

					int FirstIndex;
					byte[] UserSecretKey = TSecretKeys.GetGuidByRandom(out FirstIndex);


					var JwtParams = new Dictionary<string, object>()
					{
						{ "KullaniciId", kullanici.KullaniciId },
						{ "KisiId", kisi.KisiId },
						{ "KullaniciAdi", kullanici.KullaniciAdi },
						{ "KisiGuid", kisi.KisiGuid.Value },
						{"SonKullanmaTarihi",DateTime.Now.AddMinutes(1) }

					};

					token = Jose.JWT.Encode(JwtParams, UserSecretKey, Jose.JwsAlgorithm.HS256) + "|" + FirstIndex.ToString();
					result = true;
				}
				else
				{
					Mesaj = "Kullanıcı adı veya şifrenizi kontrol edin.";
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public TblKisi TokendanKisiye (string JWT, out TblKullanici Kullanici)
		{
            TblKisi result = null;
            Kullanici = null;

            try
			{
				if (JWT == null)
				{
					return result;
				}
                var SplittedText = JWT.Split('|');
                if (SplittedText.Length == 2)
                {
                    string Token = SplittedText[0];
                    string StrSecretKeyIndex = SplittedText[1];
                    int SecretKeyIndex = int.Parse(StrSecretKeyIndex);

                    byte[] UserSecretKey = TSecretKeys.GetGuidByteArrayByIndex(SecretKeyIndex);

                    string OpenText = Jose.JWT.Decode(Token, UserSecretKey, Jose.JwsAlgorithm.HS256);
                    Dictionary<string, object> Values =
                            JsonConvert.
                            DeserializeObject<Dictionary<string, object>>
                            (OpenText);
					if (Values.Count > 0)
					{
						int KullaniciId = 0;
						object ObjectKullaniciId;
						int KisiId = 0;
                        object ObjectKisiId;

						if (Values.TryGetValue("KullaniciId", out ObjectKullaniciId))
                            KullaniciId = int.Parse(ObjectKullaniciId.ToString());
						
                        if (Values.TryGetValue("KisiId", out ObjectKisiId))
                            KisiId = int.Parse(ObjectKisiId.ToString());

						result = (from data in Context.TblKisi where data.KisiId == KisiId select data).FirstOrDefault();
                        
						Kullanici = (from data in Context.TblKullanici where data.KisiId == result.KisiId select data).FirstOrDefault();
					}
                }
            }
			catch (Exception ex)
			{

			}
			return result;
		}

		public bool Register(TblKullanici Kullanici, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			string KullaniciAdi = Kullanici.KullaniciAdi;
			try
			{
				bool VarMi = (from data in Context.TblKullanici
							  where data.KullaniciAdi == KullaniciAdi
							  select data).Any();
				if (VarMi)
				{
					Mesaj = "Bu kullanıcı adı tanımlı";
				}
				else
				{
					Context.TblKisi.Add(Kullanici.TblKisi);
					Context.SaveChanges();
					Kullanici.KisiId = Kullanici.TblKisi.KisiId;
					Context.TblKullanici.Add(Kullanici);
					Context.SaveChanges();

				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool KullaniciSil(int KullaniciId, int KisiId, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				bool VarMi = (from data in Context.TblKullanici
							  where data.KullaniciId == KullaniciId
							  select data).Any();
				if (VarMi)
				{
					Context.TblKullanici.Remove(new DataLayer.TblKullanici()
					{ KullaniciId = KullaniciId });
					Context.TblKisi.Remove(new DataLayer.TblKisi()
					{ KisiId = KisiId });
					Context.SaveChanges();
					result = true;
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool PasswordChange(int KullaniciId, string GecerliSifre, string YeniSifre, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var Kullanici = (from data in Context.TblKullanici
								 where data.KullaniciId == KullaniciId
								 select data).FirstOrDefault();
				if (Kullanici != null)
				{
					bool PasswordIsTrue = GecerliSifre == Kullanici.Sifre ? true : false;
					if (PasswordIsTrue)
					{
						Kullanici.Sifre = YeniSifre;
						Context.SaveChanges();
						result = true;
					}
					else
						Mesaj = "Hatalı şifre girdiniz";
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool KullaniciEdit(int KullaniciId, TblKullanici Kullanici, TblKisi Kisi, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var FindKullanici = (from data in Context.TblKullanici
									 where data.KullaniciId == KullaniciId
									 select data).FirstOrDefault();

				if (FindKullanici != null)
				{
					FindKullanici.KullaniciAdi = Kullanici.KullaniciAdi;
					FindKullanici.Sifre = Kullanici.Sifre;

					var FindKisi = (from data in Context.TblKisi
									where data.KisiId == FindKullanici.KisiId
									select data).FirstOrDefault();

					if (FindKisi != null)
					{
						FindKisi.Ad = Kisi.Ad;
						FindKisi.Soyad = Kisi.Soyad;
						FindKisi.MailAdresi = Kisi.MailAdresi;
					}

					Context.SaveChanges();
					result = true;
				}
				else
				{
					Mesaj = "Kullanıcı bulunamadı";
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

        public List<TblKullanici> LastUsers()
        {
            try
            {
                var kullanici = (from data in Context.TblKullanici
                                    orderby data.KullaniciId descending
                                    select data)
                                  .ToList();
                return kullanici;
            }
            catch (Exception ex)
            {
                return new List<TblKullanici>();
            }
        }

        public List<TblKullanici> SonEklenenKullanicilar()
		{
			try
			{
				var last5Users = (from data in Context.TblKullanici
								  orderby data.KullaniciId descending
								  select data)
								 .Take(5)
								 .ToList();
				return last5Users;
			}
			catch (Exception ex)
			{
				return new List<TblKullanici>();
			}
		}

		public List<TblKullanici> GetAllUsersOrderedById()
		{
			try
			{
				var allUsers = (from data in Context.TblKullanici
								orderby data.KullaniciId ascending
								select data)
							   .ToList();
				return allUsers;
			}
			catch (Exception ex)
			{
				return new List<TblKullanici>();
			}
		}

		public List<TblMovieComment> GetUserMovieComments(int KullaniciId)
		{
			try
			{
				var userMovieComments = (from data in Context.TblMovieComment
									where data.KullaniciId == KullaniciId
									orderby data.MovieCommentDt descending
									select data)
								   .ToList();

				return userMovieComments;
			}
			catch (Exception ex)
			{
				return new List<TblMovieComment>();
			}
		}

		public List<TblTvSeriesComment> GetUserSeriesComments(int KullaniciId)
		{
			try
			{
				var userSeriesComments = (from data in Context.TblTvSeriesComment
										 where data.KullaniciId == KullaniciId
										 orderby data.TvSeriesCommentDt descending
										 select data)
								         .ToList();

				return userSeriesComments;
			}
			catch (Exception ex)
			{
				return new List<TblTvSeriesComment>();
			}
		}
	}
}