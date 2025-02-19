using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public class TTvSeriesComment : DbProcess, IDatabase
	{
		public TTvSeriesComment()
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

		public bool TvSeriesAddComment(int TvSeriesId, TblTvSeriesComment TComment, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var FindMovie = (from data in Context.TblTvSeries where data.TvSeriesId == TvSeriesId select data).FirstOrDefault();
				if (FindMovie != null)
				{
					TComment.TvSeriesId = TvSeriesId;
					TComment.TvSeriesCommentDt = DateTime.Now;
					Context.TblTvSeriesComment.Add(TComment);
					Context.SaveChanges();
					result = true;
				}
				else
				{
					Mesaj = "TvSeries is not found";
				}
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

        public List<TblTvSeriesComment> GetSeriesCommentNew()
        {
            try
            {
				var seriescomment = (from data in Context.TblTvSeriesComment
									 orderby data.TvSeriesCommentId descending
									 select data)
                                  .ToList();
                return seriescomment;
            }
            catch (Exception ex)
            {
                return new List<TblTvSeriesComment>();
            }
        }

        public List<TblTvSeriesComment> GetCommentsForTvSeries(int TvSeriesId)
		{
			List<TblTvSeriesComment> result = null;
			try
			{
				result = (from data in Context.TblTvSeriesComment
										where data.TvSeriesId == TvSeriesId
									 orderby data.TvSeriesCommentDt descending
									 select data).ToList();


			}
			catch (Exception ex)
			{
				return new List<TblTvSeriesComment>();
			}

			return result;
		}
        public bool TvSeriesCommentDelete(int TvSeriesId, int KullaniciId, out string Mesaj)
        {
            bool result = false;
            Mesaj = string.Empty;

            try
            {
                var FindComment = (from data in Context.TblTvSeriesComment
                                   where data.TvSeriesId == TvSeriesId && data.KullaniciId == KullaniciId
                                   select data).FirstOrDefault();

                if (FindComment == null)
                {
                    Mesaj = "Yorum bulunamadı veya silme izniniz yok.";
                    return false;
                }

                Context.Database.ExecuteSqlCommand("delete from TvSeriesComment.TblTvSeriesComment where TvSeriesCommentId " + TvSeriesId.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }

        public bool TvSeriesCommentUpdate(int TvSeriesCommentId, string NewTvSeriesComment, out string Mesaj)
        {
            bool result = false;
            Mesaj = string.Empty;

            try
            {
                var Comment = (from data in Context.TblTvSeriesComment
                               where data.TvSeriesCommentId == TvSeriesCommentId
                               select data).FirstOrDefault();

                if (Comment == null)
                {
                    Mesaj = "Yorum bulunamadı.";
                    return false;
                }

                Comment.TvSeriesComment = NewTvSeriesComment;
                Context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }

            return result;
        }

		public List<TblTvSeriesComment> GetLastTvSeriesComments()
		{
			try
			{
				var lastComments = (from data in Context.TblTvSeriesComment
									orderby data.TvSeriesCommentDt descending
									select data)
									 .Take(2)
									 .ToList();
				return lastComments;
			}
			catch (Exception ex)
			{
				return new List<TblTvSeriesComment>();
			}
		}

	}
}

