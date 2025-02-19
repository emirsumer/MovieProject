using Microsoft.AspNetCore.Mvc;
using Layer.Business;
using DataLayer;
using Microsoft.VisualBasic;
using MovieProject.Models;

namespace MovieProject.Controllers
{
    public class CategoryController : Controller
    {

        public IActionResult Index()
        {
            MovieSeriesDto movieSeriesDto = new MovieSeriesDto();

            TTvSeries tvseries = new TTvSeries();
            TMovie movie = new TMovie();
            TCategory category = new TCategory();

            movieSeriesDto.movies = movie.MovieList();
            movieSeriesDto.tvSeries = tvseries.TvSeriesList();
            movieSeriesDto.tblCategories = category.CategoryList();

            return View(movieSeriesDto);
        }
        public IActionResult CategoryList()
        {
            MovieSeriesDto movieSeriesDto = new MovieSeriesDto();

            TTvSeries tvseries = new TTvSeries();
            TMovie movie = new TMovie();
            TCategory category = new TCategory();

            movieSeriesDto.movies = movie.MovieList();
            movieSeriesDto.tvSeries = tvseries.TvSeriesList();
            movieSeriesDto.tblCategories = category.CategoryList();

            return View(movieSeriesDto);
        }

        public IActionResult MovieDetails(int id)
        {
            int KullaniciId = 0;
            int KisiId = 0;
            TblMoviePhoto MovieInformation = null;
            ViewBag.MovieInformation = null;

            TMovie movie = new();
            var moovieDetail = movie.MovieDetail(id, KullaniciId, KisiId);

            TMovieComment comment = new TMovieComment();
            var movieComments = comment.GetCommentsForMovie(id);

            MovieDetail movieDetailDto = new MovieDetail();

            movieDetailDto.Moviess = moovieDetail;
            movieDetailDto.BannerUrl = moovieDetail.TblMoviePhoto.FirstOrDefault().MoviePhotoUrl;
            movieDetailDto.VideoUrl = moovieDetail.TblMovieVideo.FirstOrDefault().MovieVideoUrl;
            movieDetailDto.Movies = movie.LastAddMovieList(5);
            movieDetailDto.MoviePhotos = moovieDetail.TblMoviePhoto.ToList();
            movieDetailDto.movieComments = comment.GetCommentsForMovie(id);


            if (moovieDetail != null)
            {
                return View(movieDetailDto);
            }
            else
            {
                return View("~/Error/Index");
            }

        }

        public IActionResult TvSeriesDetails(int id)
        {
            int KullaniciId = 0;
            int KisiId = 0;
            TblTvSeriesPhoto TvSeriesInformation = null;
            ViewBag.TvSeriesInformation = null;

            TTvSeries series = new();
            var seriesDetail = series.TvSeriesDetail(id, KullaniciId, KisiId);

            TTvSeriesComment seriesComment = new();
            var SeriesComment = seriesComment.GetCommentsForTvSeries(id);
                ;
            TvSeriesDetail tvSeriesDto = new();

            tvSeriesDto.TvSeries = seriesDetail;
            tvSeriesDto.BannerUrl = seriesDetail.TblTvSeriesPhoto.FirstOrDefault().TvSeriesPhotoUrl;
            tvSeriesDto.VideoUrl = seriesDetail.TblTvSeriesVideo.FirstOrDefault().TvSeriesVideoUrl;
            tvSeriesDto.AllTvSeries = series.LastAddTvSeriesList(5);
            tvSeriesDto.SeriesComments = seriesComment.GetCommentsForTvSeries(id);

            if (tvSeriesDto != null)
            {
                return View(tvSeriesDto);
            }
            else
            {
                return View("~/Error/Index");
            }

        }

        [HttpGet]
        public IActionResult Categories()
        {
            TCategory category = new TCategory();
            var Categories = category.CategoryList();
            return Json(Categories);
        }


       

        [HttpPost]
        public IActionResult AddMovieComment(int movieId, string commentText)
        {

            TMovieComment movieComment = new();
            TblMovieComment tblMovieComment = new();
            tblMovieComment.MovieId = movieId;
            tblMovieComment.MovieComment = commentText;

            TMovie movie = new();


            string message;
            bool success = movieComment.MovieAddComment(movieId, tblMovieComment, out message);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpDelete]
        public IActionResult MovieCommentDelete(int movieId, int MovieCommentId)
        {
            TMovieComment movieComment = new TMovieComment();
            string message;
            bool success = movieComment.MovieCommentDelete(movieId, MovieCommentId, out message);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }




        [HttpPut]
        public IActionResult UpdateMovieComment(int MovieCommentId, string newCommentText)
        {
            TMovieComment movie = new TMovieComment();
            string message;
            bool success = movie.MovieCommentUpdate(MovieCommentId, newCommentText, out message);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        public IActionResult TvSeriesCommentDelete(int TvSeriesId, int TvSeriesCommentId)
        {
            TTvSeriesComment tvSeriesComment = new();
            string message;
            bool success = tvSeriesComment.TvSeriesCommentDelete(TvSeriesId, TvSeriesCommentId, out message);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateTvSeriesComment(int TvSeriesCommentId, string newCommentText)
        {
            TTvSeriesComment tvSeriesComment = new();
            string message;
            bool success = tvSeriesComment.TvSeriesCommentUpdate(TvSeriesCommentId, newCommentText, out message);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetTvSeriesComments(int TvSeriesId)
        {
            TTvSeriesComment tvSeriesComment = new();
            string message;
            var comments = tvSeriesComment.GetCommentsForTvSeries(TvSeriesId);

            if (comments != null && comments.Count > 0)
            {
                return Ok(comments);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult ActorDetail(string id)
        {
            TActor actor = new();
            var ActorDetail = actor.GetActorDetail(Convert.ToInt32(id));

            ActorDetails actorDetailDto = new();
            actorDetailDto.Actor = ActorDetail.Actor;
            actorDetailDto.TvSeries = ActorDetail.TvSeries;
            actorDetailDto.Movies = ActorDetail.Movies;
            if (ActorDetail != null)
            {
                return View(actorDetailDto);
            }
            else
            {
                return View("~/Error/Index");
            }

        }

    }
}
