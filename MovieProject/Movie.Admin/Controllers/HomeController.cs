using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLayer;
using Layer.Business;
using Movie.Admin.Models;
using System.Xml.Linq;


namespace Movie.Admin.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            MovieSeries movieSeries = new();


            TTvSeries tvseries = new();
            TMovie movie = new();
            TKullanici kullanici = new();
            TMovieComment movieComment = new();
            TTvSeriesComment tvSeriesComment = new();


            movieSeries.movies = movie.GetLastAddedMovies();
            movieSeries.tvSeries = tvseries.GetLastAddedTvSeries();
            movieSeries.kullanicilar = kullanici.SonEklenenKullanicilar();
            movieSeries.moviesComments = movieComment.GetLastMovieComments();
            movieSeries.seriesComment = tvSeriesComment.GetLastTvSeriesComments();

            movieSeries.Movies = movie.GetAllMoviesOrderedById();
            movieSeries.TvSeries = tvseries.GetAllSeriesOrderedById();
            movieSeries.MoviesComments = movieComment.GetMovieCommentNew();
            movieSeries.SeriesComment = tvSeriesComment.GetSeriesCommentNew();

            var MovieCount = movieSeries.Movies.Count();
            var SeriesCount = movieSeries.TvSeries.Count();
            var movieCommentCount = movieSeries.MoviesComments.Count();
            var seriesCommentCount = movieSeries.SeriesComment.Count();


            ViewBag.MovieCount = MovieCount;
            ViewBag.SeriesCount = SeriesCount;
            ViewBag.MCommentCount = movieCommentCount;
            ViewBag.TvCommentCount = seriesCommentCount;

            return View(movieSeries);
        }
    }
}
