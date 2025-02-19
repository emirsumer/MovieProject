using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
    public class TMovie : DbProcess, IDatabase
    {
        public TMovie()
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

        public bool MovieAdd(TblMovie Movie, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                Movie.MovieSeo = Movie.MovieName.MCreateSeo();
                Context.TblMovie.Add(Movie);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }

        public bool MovieAdd(TblMovie Movie, List<TblCategory> categories, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                Movie.MovieSeo = Movie.MovieName.MCreateSeo();
                Context.TblMovie.Add(Movie);
                Context.SaveChanges();
                int MovieId = Movie.MovieId;

                foreach (var category in categories)
                    category.CategoryId = MovieId;

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
        public bool MoviesAdd(List<TblMovie> Movie, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {

                Context.TblMovie.AddRange(Movie);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }

        public bool MovieEdit(int MovieId, TblMovie Movie, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                var FindMovie = (from data in Context.TblMovie
                                 where data.MovieId == Movie.MovieId
                                 select data).FirstOrDefault();
                if (FindMovie != null)
                {
                    Movie.MovieSeo = Movie.MovieName.MCreateSeo();
                    FindMovie = Movie;
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

        public bool MovieEdit(int MovieId, int CategoryId, TblMovie Movie, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                var FindMovie = (from data in Context.TblMovie
                                 where data.MovieId == Movie.MovieId &&
                                 data.CategoryId == Movie.CategoryId
                                 select data).FirstOrDefault();
                if (FindMovie != null)
                {
                    Movie.MovieSeo = Movie.MovieName.MCreateSeo();
                    FindMovie = Movie;
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
        
        

        public bool DeleteMovie(int MovieId, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            try
            {
                Context.Database.ExecuteSqlCommand("delete from TblCategory where CategoryId=" + MovieId.ToString());
                Context.Database.ExecuteSqlCommand("delete from TblMovie where CategoryId=" + MovieId.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }

        public bool MovieCategoryAdd(TblMovie Movie, out string Mesaj)
        {
            bool result = false;
            Mesaj = "";
            string movie = Movie.MovieName;
            int category = Movie.CategoryId;
            try
            {
                bool movieCategory = (from data in Context.TblMovie
                                      where data.MovieName == movie
                                      select data).Any();

                bool existingCategory = (from data in Context.TblCategory
                                         where data.CategoryId == category
                                         select data).Any();
                if (movieCategory && existingCategory)
                {
                    Mesaj = "Bu film kayıtlı";
                }
                else
                {
                    Context.TblMovie.Add(Movie);
                    Context.SaveChanges();
                    Movie.MovieId = Movie.TblCategory.CategoryId;
                    Context.TblCategory.Add(Movie.TblCategory);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Mesaj = ex.Message;
            }
            return result;
        }
        public IQueryable<TblMovie> CategoryMovieList(int CategoryId, out IQueryable<TblMoviePhoto> MoviePhotos)
        {
            MoviePhotos = null;
            IQueryable<TblMovie> result = null;
            var MovieIds = (from data in Context.TblCategory where data.CategoryId == CategoryId select data).ToArray();
            if (MovieIds.Length > 0)
            {
                var MovieList = (from data in Context.TblMovie select data).ToList();
                result = MovieList.
                    Where(m => m.MovieId.ToString()
                    .Contains(m.MovieId.ToString())).AsQueryable();

                MoviePhotos = (from data in Context.TblMoviePhoto select data).ToList()
                    .Where(m => m.MovieId.ToString()
                    .Contains(m.MovieId.ToString())).AsQueryable();


            }
            return result;
        }
        public List<MoviePhotoVideo> MovieList()
        {
            List<MoviePhotoVideo> result = null;
            var Movies = (from data in Context.TblMovie orderby data.Goruntulenme descending select data).ToList();

            if (Movies.Count > 0)
            {
                result = new List<MoviePhotoVideo>();
                foreach (var Movie in Movies)
                {
                    MoviePhotoVideo MPhotoVideo = new MoviePhotoVideo();
                    MPhotoVideo.Movie = Movie;

                    int commentCount = GetCommentCountForMovie(Movie.MovieId);
                    MPhotoVideo.CommentCount = commentCount;

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


                    var MVideoData = (from data in Context.TblMovieVideo
                                      where data.MovieId == Movie.MovieId
                                      orderby data.MovieVideoId descending
                                      select data).FirstOrDefault();
                    if (MVideoData != null)
                    {
                        string MovieVideo = MVideoData.MovieVideoUrl;
                        MVideoData.MovieVideoUrl = MovieVideo;
                    }



                    var MPhotos = (from data in Context.TblMoviePhoto
                                   where data.MovieId == Movie.MovieId &&
                                   data.MovieAcilis.Value != true
                                   orderby data.MoviePhotoId descending
                                   select data).ToList();
                    MPhotoVideo.MoviePhotos.AddRange(MPhotos);
                    MPhotoVideo.MovieVideos.Add(MVideoData);
                    result.Add(MPhotoVideo);
                }
            }

            return result;
        }
        public List<MoviePhotoVideo> MovieList(string SearchedWord)
        {
            List<MoviePhotoVideo> result = null;
            var Movies = (from data in Context.TblMovie
                          where data.MovieName.Contains(SearchedWord)
                          orderby data.Goruntulenme descending
                          select data).ToList();

            if (Movies.Count > 0)
            {
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


                    var MVideoData = (from data in Context.TblMovieVideo
                                      where data.MovieId == Movie.MovieId
                                      orderby data.MovieVideoId descending
                                      select data).FirstOrDefault();
                    if (MVideoData != null)
                    {
                        string MovieVideo = MVideoData.MovieVideoUrl;
                        MVideoData.MovieVideoUrl = MovieVideo;
                    }



                    var MPhotos = (from data in Context.TblMoviePhoto
                                   where data.MovieId == Movie.MovieId &&
                                   data.MovieAcilis.Value != true
                                   orderby data.MoviePhotoId descending
                                   select data).ToList();
                    MPhotoVideo.MoviePhotos.AddRange(MPhotos);
                    MPhotoVideo.MovieVideos.Add(MVideoData);
                }
            }

            return result;
        }
        public TblMovie MovieDetail(int MovieId, int KullaniciId = 0, int KisiId = 0)
        {
            //MoviePhotoVideo result = new MoviePhotoVideo();
            var MovieDetail = (from data in Context.TblMovie
                               where data.MovieId == MovieId
                               orderby data.Goruntulenme descending
                               select data).FirstOrDefault();
            MovieDetail.TblMoviePhoto = (from data in TblMoviePhoto where data.MovieId == MovieDetail.MovieId select data).ToList();
            MovieDetail.TblMovieVideo = (from data in TblMovieVideo where data.MovieId == MovieId select data).ToList();
            MovieDetail.TblActor = (from data in Context.TblActor where data.MovieId == MovieDetail.MovieId select data).ToList();
            MovieDetail.TblDirector = (from data in Context.TblDirector where data.MovieId == MovieDetail.MovieId select data).ToList();

            if (KullaniciId > 0 || KisiId > 0)
            {
                bool MGezmisMi = MDahaOnceGezmisMi(KullaniciId, KisiId, MovieDetail.MovieId);
                if (!MGezmisMi)
                {
                    MovieDetail.Goruntulenme++;
                    Context.SaveChanges();
                    KisiGezintiMovieEkle(MovieDetail, KisiId, KullaniciId);
                }

            }

            //result.Movie = MovieDetail;




            return MovieDetail;
        }
        private bool MDahaOnceGezmisMi(int KullaniciId, int KisiId, int MovieId)
        {
            bool result = (from data in Context.TblKisiGezinti
                           where (data.KullaniciId.Value == KullaniciId ||
                           data.KisiId.Value == KisiId) && data.MovieId.Value == MovieId
                           select data).AsNoTracking().Any();
            return result;
        }
        public List<MoviePhotoVideo> LastAddMovieList(int HowMany)
        {
            List<MoviePhotoVideo> result = null;
            var Movies = (from data in Context.TblMovie
                          orderby data.MovieId descending
                          select data).Take(5).ToList();

            if (Movies.Count > 0)
            {
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


                    var MVideoData = (from data in Context.TblMovieVideo
                                      where data.MovieId == Movie.MovieId
                                      orderby data.MovieVideoId descending
                                      select data).FirstOrDefault();
                    if (MVideoData != null)
                    {
                        string MovieVideo = MVideoData.MovieVideoUrl;
                        MVideoData.MovieVideoUrl = MovieVideo;
                    }



                    var MPhotos = (from data in Context.TblMoviePhoto
                                   where data.MovieId == Movie.MovieId &&
                                   data.MovieAcilis.Value != true
                                   orderby data.MoviePhotoId descending
                                   select data).ToList();
                    MPhotoVideo.MoviePhotos.AddRange(MPhotos);
                    MPhotoVideo.MovieVideos.Add(MVideoData);
                    result.Add(MPhotoVideo);

                }
            }

            return result;
        }

        private void KisiGezintiMovieEkle(TblMovie Movie, int KisiId = 0, int KullaniciId = 0)
        {
            TblKisiGezinti gezintiMovie = new DataLayer.TblKisiGezinti();
            gezintiMovie.KisiId = KisiId;
            gezintiMovie.KullaniciId = KullaniciId;
            gezintiMovie.MovieId = Movie.MovieId;
            Context.TblKisiGezinti.Add(gezintiMovie);
            Context.SaveChanges();
        }

        public List<TblMovie> GetLastAddedMovies()
        {
            try
            {
                var last5Movies = (from data in Context.TblMovie
                                   orderby data.MovieId descending
                                   select data)
                                  .Take(5)
                                  .ToList();
                return last5Movies;
            }
            catch (Exception ex)
            {
                return new List<TblMovie>();
            }
        }

        public List<TblMovie> GetAllMoviesOrderedById()
        {
            try
            {
                var allMovies = (from data in Context.TblMovie
                                 orderby data.MovieId ascending
                                 select data)
                                .ToList();
                return allMovies;
            }
            catch (Exception ex)
            {
                return new List<TblMovie>();
            }
        }

        public int GetCommentCountForMovie(int MovieId)
        {
            try
            {
                var commentCount = (from data in Context.TblMovieComment
                                    where data.MovieId == MovieId
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
