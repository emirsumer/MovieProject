using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public class TTvSeries : DbProcess, IDatabase
	{
		public TTvSeries()
		{
			Context = new DbMovieEntities();
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
		public bool TvSeriesAdd(TblTvSeries TvSeries, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				TvSeries.TvSeriesSeo = TvSeries.TvSeriesName.TvCreateSeo();
				Context.TblTvSeries.Add(TvSeries);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool TvSeriesAdd(TblTvSeries TvSeries, List<TblCategory> categories, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				TvSeries.TvSeriesSeo = TvSeries.TvSeriesName.TvCreateSeo();
				Context.TblTvSeries.Add(TvSeries);
				Context.SaveChanges();
				int TvSeriesId = TvSeries.TvSeriesId;

				foreach (var category in categories)
					category.CategoryId = TvSeriesId;

				Context.TblCategory.AddRange(categories);
				Context.SaveChanges();

				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool TvSeriesSAdd(List<TblTvSeries> TvSeries, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{

				Context.TblTvSeries.AddRange(TvSeries);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool TvSeriesEdit(int TvSeriesId, TblTvSeries TvSeries, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var FindTvSeries = (from data in Context.TblTvSeries
									where data.TvSeriesId == TvSeries.TvSeriesId
									select data).FirstOrDefault();
				if (FindTvSeries != null)
				{
					TvSeries.TvSeriesSeo = TvSeries.TvSeriesName.TvCreateSeo();
					FindTvSeries = TvSeries;
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

		public bool TvSeriesEdit(int TvSeriesId, int CategoryId, TblTvSeries TvSeries, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var FindTvSeries = (from data in Context.TblTvSeries
									where data.TvSeriesId == TvSeries.TvSeriesId &&
								 data.CategoryId == TvSeries.CategoryId
									select data).FirstOrDefault();
				if (FindTvSeries != null)
				{
					TvSeries.TvSeriesSeo = TvSeries.TvSeriesName.TvCreateSeo();
					FindTvSeries = TvSeries;
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

		public bool TvSeriesDelete(int TvSeriesId, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				//Context.TblCategory.Remove(new DataLayer.TblCategory()
				//{ CategoryId = TvSeriesId }
				//);
				Context.Database.ExecuteSqlCommand("delete from TblCategory where CategoryId=" + TvSeriesId.ToString());
				Context.SaveChanges();

				//Context.TblTvSeries.Remove(new DataLayer.TblTvSeries()
				//{ TvSeriesId = TvSeriesId }
				//);
				Context.Database.ExecuteSqlCommand("delete from TblTvSeries where CategoryId=" + TvSeriesId.ToString());

				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool TvSeriesCategoryAdd(TblTvSeries TvSeries, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			string tvSeries = TvSeries.TvSeriesName;
			int category = TvSeries.CategoryId;
			try
			{
				bool tvSeriesCategory = (from data in Context.TblTvSeries
										 where data.TvSeriesName == tvSeries
										 select data).Any();

				bool existingCategory = (from data in Context.TblCategory
										 where data.CategoryId == category
										 select data).Any();
				if (tvSeriesCategory && existingCategory)
				{
					Mesaj = "Bu dizi kayıtlı";
				}
				else
				{
					Context.TblTvSeries.Add(TvSeries);
					Context.SaveChanges();
					TvSeries.TvSeriesId = TvSeries.TblCategory.CategoryId;
					Context.TblCategory.Add(TvSeries.TblCategory);
					Context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}
		public IQueryable<TblTvSeries> CategoryTvSeriesList(int CategoryId, out IQueryable<TblTvSeriesPhoto> TvSeriesPhotos)
		{
			TvSeriesPhotos = null;
			IQueryable<TblTvSeries> result = null;
			var TvSeriesIds = (from data in Context.TblCategory where data.CategoryId == CategoryId select data).ToArray();
			if (TvSeriesIds.Length > 0)
			{
				var TvSeriesList = (from data in Context.TblTvSeries select data).ToList();
				result = TvSeriesList.
					Where(t => t.TvSeriesId.ToString()
					.Contains(t.TvSeriesId.ToString())).AsQueryable();

				TvSeriesPhotos = (from data in Context.TblTvSeriesPhoto select data).ToList()
					.Where(t => t.TvSeriesId.ToString()
					.Contains(t.TvSeriesId.ToString())).AsQueryable();


			}
			return result;
		}
		public List<TvSeriesPhotoVideo> TvSeriesList()
		{
			List<TvSeriesPhotoVideo> result = null;
			var TvSeriess = (from data in Context.TblTvSeries orderby data.Goruntulenme descending select data).ToList();

			if (TvSeriess.Count > 0)
			{
				result = new List<TvSeriesPhotoVideo>();
				foreach (var TvSeries in TvSeriess)
				{
					TvSeriesPhotoVideo TPhotoVideo = new TvSeriesPhotoVideo();
					TPhotoVideo.TvSeries = TvSeries;

                    int commentCount = GetCommentCountForSeries(TvSeries.TvSeriesId);
                    TPhotoVideo.CommentCount = commentCount;

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


					var TVideoData = (from data in Context.TblTvSeriesVideo
									  where data.TvSeriesId == TvSeries.TvSeriesId
									  orderby data.TvSeriesVideoId descending
									  select data).FirstOrDefault();
					if (TVideoData != null)
					{
						string TvSeriesVideo = TVideoData.TvSeriesVideoUrl;
						TVideoData.TvSeriesVideoUrl = TvSeriesVideo;
					}



					var TPhotos = (from data in Context.TblTvSeriesPhoto
								   where data.TvSeriesId == TvSeries.TvSeriesId &&
								   data.TvSeriesAcilis.Value != true
								   orderby data.TvSeriesPhotoId descending
								   select data).ToList();
					TPhotoVideo.TvSeriesPhotos.AddRange(TPhotos);
					TPhotoVideo.TvSeriesVideos.Add(TVideoData);
                    result.Add(TPhotoVideo);

                }
            }

			return result;
		}
		public List<TvSeriesPhotoVideo> TvSeriesList(string SearchedWord)
		{
			List<TvSeriesPhotoVideo> result = null;
			var TvSeriess = (from data in Context.TblTvSeries
							 where data.TvSeriesName.Contains(SearchedWord)
							 orderby data.Goruntulenme descending
							 select data).ToList();

			if (TvSeriess.Count > 0)
			{
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


					var TVideoData = (from data in Context.TblTvSeriesVideo
									  where data.TvSeriesId == TvSeries.TvSeriesId
									  orderby data.TvSeriesVideoId descending
									  select data).FirstOrDefault();
					if (TVideoData != null)
					{
						string TvSeriesVideo = TVideoData.TvSeriesVideoUrl;
						TVideoData.TvSeriesVideoUrl = TvSeriesVideo;
					}



					var TPhotos = (from data in Context.TblTvSeriesPhoto
								   where data.TvSeriesId == TvSeries.TvSeriesId &&
								   data.TvSeriesAcilis.Value != true
								   orderby data.TvSeriesPhotoId descending
								   select data).ToList();
					TPhotoVideo.TvSeriesPhotos.AddRange(TPhotos);
					TPhotoVideo.TvSeriesVideos.Add(TVideoData);

                }
            }
            return result;

        }
        public TblTvSeries TvSeriesDetail(int TvSeriesId, int KullaniciId = 0, int KisiId = 0)
		{
			//TvSeriesPhotoVideo result = new TvSeriesPhotoVideo();
			var TvSeriesDetail = (from data in Context.TblTvSeries
								  where data.TvSeriesId == TvSeriesId
                                  orderby data.Goruntulenme descending
								  select data).FirstOrDefault();
            TvSeriesDetail.TblActor = (from data in Context.TblActor where data.TvSeriesId == TvSeriesDetail.TvSeriesId select data).ToList();
            TvSeriesDetail.TblTvSeriesPhoto = (from data in TblTvSeriesPhoto where data.TvSeriesId == TvSeriesDetail.TvSeriesId select data).ToList();
            TvSeriesDetail.TblTvSeriesVideo = (from data in TblTvSeriesVideo where data.TvSeriesId == TvSeriesDetail.TvSeriesId select data).ToList();
            TvSeriesDetail.TblActor = (from data in Context.TblActor where data.TvSeriesId == TvSeriesDetail.TvSeriesId select data).ToList();
            TvSeriesDetail.TblDirector = (from data in Context.TblDirector where data.TvSeriesId == TvSeriesDetail.TvSeriesId select data).ToList();

            if (KullaniciId > 0 || KisiId > 0)
			{
				bool TGezmisMi = TDahaOnceGezmisMi(KullaniciId, KisiId, TvSeriesDetail.TvSeriesId);
				if (!TGezmisMi)
				{
					TvSeriesDetail.Goruntulenme++;
					Context.SaveChanges();
					KisiGezintiTvSeriesEkle(TvSeriesDetail,KisiId,KullaniciId);
				}
			}

			return TvSeriesDetail;

			//var TPhotoData = (from data in Context.TblTvSeriesPhoto
			//				  where data.TvSeriesId == TvSeriesDetail.TvSeriesId &&
			//				  data.TvSeriesAcilis.Value == true
			//				  orderby data.TvSeriesPhotoId descending
			//				  select data).FirstOrDefault();
			//if (TPhotoData != null)
			//{
			//	string TSOpeningImage = TPhotoData.TvSeriesPhotoUrl;
			//	TPhotoData.TvSeriesPhotoUrl = TSOpeningImage;
			//}
			//var TVideoData = (from data in Context.TblTvSeriesVideo
			//				  where data.TvSeriesId == TvSeriesDetail.TvSeriesId
			//				  orderby data.TvSeriesVideoId descending
			//				  select data).FirstOrDefault();
			//if (TVideoData != null)
			//{
			//	string TvSeriesVideo = TVideoData.TvSeriesVideoUrl;
			//	TVideoData.TvSeriesVideoUrl = TvSeriesVideo;
			//}

			//var TPhotos = (from data in Context.TblTvSeriesPhoto
			//			   where data.TvSeriesId == TvSeriesDetail.TvSeriesId &&
			//			   data.TvSeriesAcilis.Value != true
			//			   orderby data.TvSeriesPhotoId descending
			//			   select data).ToList();
			//result.TvSeriesPhotos.AddRange(TPhotos);
			//result.TvSeriesVideos.Add(TVideoData);

		}
		private bool TDahaOnceGezmisMi(int KullaniciId, int KisiId, int TvSeriesId)
		{
			bool result = (from data in Context.TblKisiGezinti
						   where (data.KullaniciId.Value == KullaniciId ||
						   data.KisiId.Value == KisiId) && data.TvSeriesId.Value == TvSeriesId
						   select data).AsNoTracking().Any();
			return result;
		}
		public List<TvSeriesPhotoVideo> LastAddTvSeriesList(int HowMany)
		{
			List<TvSeriesPhotoVideo> result = null;
			var TvSeriess = (from data in Context.TblTvSeries
							 orderby data.TvSeriesId descending
							 select data).Take(5).ToList();

			if (TvSeriess.Count > 0)
			{
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


					var TVideoData = (from data in Context.TblTvSeriesVideo
									  where data.TvSeriesId == TvSeries.TvSeriesId
									  orderby data.TvSeriesVideoId descending
									  select data).FirstOrDefault();
					if (TVideoData != null)
					{
						string TvSeriesVideo = TVideoData.TvSeriesVideoUrl;
						TVideoData.TvSeriesVideoUrl = TvSeriesVideo;
					}



					var TPhotos = (from data in Context.TblTvSeriesPhoto
								   where data.TvSeriesId == TvSeries.TvSeriesId &&
								   data.TvSeriesAcilis.Value != true
								   orderby data.TvSeriesPhotoId descending
								   select data).ToList();
					TPhotoVideo.TvSeriesPhotos.AddRange(TPhotos);
					TPhotoVideo.TvSeriesVideos.Add(TVideoData);
                    result.Add(TPhotoVideo);

                }
			}

			return result;
		}

        private void KisiGezintiTvSeriesEkle(TblTvSeries TvSeries, int KisiId = 0, int KullaniciId = 0)
        {
            TblKisiGezinti gezintiTvSeries = new DataLayer.TblKisiGezinti();
            gezintiTvSeries.KisiId = KisiId;
            gezintiTvSeries.KullaniciId = KullaniciId;
            gezintiTvSeries.TvSeriesId = TvSeries.TvSeriesId;
            Context.TblKisiGezinti.Add(gezintiTvSeries);
            Context.SaveChanges();
        }

		public List<TblTvSeries> GetLastAddedTvSeries()
		{
			try
			{
				var last5Series = (from data in Context.TblTvSeries
								   orderby data.TvSeriesId descending
								   select data)
								  .Take(5)
								  .ToList();
				return last5Series;
			}
			catch (Exception ex)
			{
				return new List<TblTvSeries>();
			}
		}
		public List<TblTvSeries> GetAllSeriesOrderedById()
		{
			try
			{
				var allSeries = (from data in Context.TblTvSeries
								 orderby data.TvSeriesId ascending
								 select data)
								.ToList();
				return allSeries;
			}
			catch (Exception ex)
			{
				return new List<TblTvSeries>();
			}
		}


        public int GetCommentCountForSeries(int TvSeriesId)
        {
            try
            {
                var commentCount = (from data in Context.TblTvSeriesComment
                                    where data.TvSeriesId == TvSeriesId
                                    select data).Count();
                return commentCount;

            }
            catch (Exception)
            {
                return 0;
            }

        }

    }
}

