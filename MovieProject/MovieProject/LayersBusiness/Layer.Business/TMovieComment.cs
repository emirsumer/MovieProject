using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
    public class TMovieComment : DbProcess, IDatabase
    {
        public TMovieComment()
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

        public bool MovieAddComment(int MovieId, TblMovieComment MComment, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                var FindMovie = (from data in Context.TblMovie where data.MovieId == MovieId select data).FirstOrDefault();
                if (FindMovie != null)
                {
                    MComment.MovieId = MovieId;
                    MComment.MovieCommentDt = DateTime.Now;
                    Context.TblMovieComment.Add(MComment);
                    Context.SaveChanges();
                    result = true;
                }
                else
                {
                    Mesaj = "Movie is not found";
                }
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }


        public List<TblMovieComment> GetMovieCommentNew()
        {
            try
            {
                var moviecomment = (from data in Context.TblMovieComment
                                   orderby data.MovieCommentId descending
                                   select data)
                                  .ToList();
                return moviecomment;
            }
            catch (Exception ex)
            {
                return new List<TblMovieComment>();
            }
        }

		





		public List<TblMovieComment> GetCommentsForMovie(int MovieId)
        {
            List<TblMovieComment> result = null;
            try
            {
                result = (from data in Context.TblMovieComment
                          where data.MovieId == MovieId
                          orderby data.MovieCommentDt descending
                          select data).ToList();


            }
            catch (Exception ex)
            {
                return new List<TblMovieComment>();
            }

            return result;
        }

        public bool MovieCommentDelete(int MovieId, int KullaniciId, out string Mesaj)
        {
            bool result = false;
            Mesaj = string.Empty;

            try
            {
                var FindComment = (from data in Context.TblMovieComment
                                   where data.MovieId == MovieId && data.KullaniciId == KullaniciId
                                   select data).FirstOrDefault();

                if (FindComment == null)
                {
                    Mesaj = "Yorum bulunamadı veya silme izniniz yok.";
                    return false;
                }

                Context.Database.ExecuteSqlCommand("delete from MovieComment.TblMovieComment where MovieCommentId " + MovieId.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }

        public bool MovieCommentUpdate(int MovieCommentId, string NewMovieComment, out string Mesaj)
        {
            bool result = false;
            Mesaj = string.Empty;

            try
            {
                var Comment = (from data in Context.TblMovieComment
                               where data.MovieCommentId == MovieCommentId
                               select data).FirstOrDefault();

                if (Comment == null)
                {
                    Mesaj = "Yorum bulunamadı.";
                    return false;
                }

                Comment.MovieComment = NewMovieComment;
                Context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }

            return result;
        }

        public List<TblMovieComment> GetLastMovieComments()
        {
            try
            {
                var last5Comments = (from data in Context.TblMovieComment
                                     orderby data.MovieCommentDt descending
                                     select data)
                                     .Take(3)
                                     .ToList();
                return last5Comments;
            }
            catch (Exception ex)
            {
                return new List<TblMovieComment>();
            }
        }

    }
}
