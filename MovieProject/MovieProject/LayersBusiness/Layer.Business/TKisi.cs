using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public class TKisi : DbProcess, IDatabase
	{
		public TKisi()
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
		public bool KisiEkle(ref TblKisi Kisi, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblKisi.Add(Kisi);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool KisiEkle(ref List<TblKisi> Kisiler, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblKisi.AddRange(Kisiler);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool KisiDuzenle(int KisiId, TblKisi Kisi, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				var KisiData = (from data in Context.TblKisi
								where data.KisiId == Kisi.KisiId
								select data).FirstOrDefault();
				if (KisiData != null)
				{
					KisiData = Kisi;
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

		public bool KisiSil(int KisiId, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblKullanici.Remove(new DataLayer.TblKullanici()
				{ KisiId = KisiId });
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}
		public bool KisiGezinti(int KisiId, int KullaniciId, int MovieId, int TvSeriesId, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				var Gezinti = (from data in Context.TblKisiGezinti
							   where (data.KullaniciId.Value == KullaniciId || data.KisiId.Value == KisiId) &&
							   (data.MovieId.Value == MovieId || data.TvSeriesId.Value == TvSeriesId)
							   select data).Any();
				if (!Gezinti)
				{
					TblKisiGezinti gezinti = new TblKisiGezinti();
					gezinti.KullaniciId = KullaniciId;
					gezinti.KisiId = KisiId;
					gezinti.MovieId = MovieId;
					gezinti.TvSeriesId = TvSeriesId;
					Context.TblKisiGezinti.Add(gezinti);
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
		public List<MoviePhotoVideo> MovieGezintiListesi(int KullaniciId, int KisiId)
		{
			List<MoviePhotoVideo> result = null;
			var MovieIds = (from data in Context.TblKisiGezinti
							where data.KullaniciId.Value == KullaniciId ||
							data.KisiId.Value == KisiId
							orderby data.MovieId descending
							select data.MovieId).ToArray();

			if (MovieIds.Length > 0)
			{
				var Movies = (from data in Context.TblMovie where MovieIds.Contains(data.MovieId) select data).ToList();
				result = new List<MoviePhotoVideo>();
				foreach (var Movie in Movies)
				{
					MoviePhotoVideo MPhotoVideo = new MoviePhotoVideo();
					MPhotoVideo.Movie = Movie;
					var MPhotoData = (from data in Context.TblMoviePhoto
									  where data.MovieId == Movie.MovieId &&
									  data.MovieAcilis.Value == true
									  orderby data.MoviePhotoId descending
									  select data).FirstOrDefault();
					if (MPhotoData != null)
					{
						string MovieOpeningImage = MPhotoData.MoviePhotoUrl;
						MPhotoData.MoviePhotoUrl = MovieOpeningImage;
					}

					//MPhotoVideo.MoviePhotos.Add(MPhotoData);
				}
			}

			return result;
		}


		public List<TvSeriesPhotoVideo> TvSeriesGezintiListesi(int KullaniciId, int KisiId)
		{
			List<TvSeriesPhotoVideo> result = null;
			var TvSeriesIds = (from data in Context.TblKisiGezinti
							   where data.KullaniciId.Value == KullaniciId ||
							   data.KisiId.Value == KisiId
							   orderby data.TvSeriesId descending
							   select data.TvSeriesId).ToArray();

			if (TvSeriesIds.Length > 0)
			{
				var TvSeriess = (from data in Context.TblTvSeries where TvSeriesIds.Contains(data.TvSeriesId) select data).ToList();

				result = new List<TvSeriesPhotoVideo>();
				foreach (var TvSeries in TvSeriess)
				{
					TvSeriesPhotoVideo TPhotoVideo = new TvSeriesPhotoVideo();
					TPhotoVideo.TvSeries = TvSeries;
					var TPhotoData = (from data in Context.TblTvSeriesPhoto
									  where data.TvSeriesId == TvSeries.TvSeriesId &&
									  data.TvSeriesAcilis.Value == true
									  orderby data.TvSeriesPhotoId descending
									  select data).FirstOrDefault();
					if (TPhotoData != null)
					{
						string TSOpeningImage = TPhotoData.TvSeriesPhotoUrl;
						TPhotoData.TvSeriesPhotoUrl = TSOpeningImage;
					}

					//TPhotoVideo.TvSeriesPhotos.Add(TPhotoData);
				}
			}

			return result;
		}

	}
}
