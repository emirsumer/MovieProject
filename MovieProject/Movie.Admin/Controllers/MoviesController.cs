using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            MovieSeriesDto movieSeries = new();
            TMovie movie = new TMovie();


            movieSeries.movies = movie.MovieList();
            return View(movieSeries);
        }
        [HttpDelete]
        public IActionResult DeleteContent(RequestCreate create)
        {
            if (create == null)
            {
                return BadRequest();
            }

            string message;
            bool success;

            if (create.Type == 1)
            {
                TMovie movieService = new();
                var movie = movieService.GetAllMoviesOrderedById();

                if (movie == null)
                {
                    return NotFound();
                }

                success = movieService.DeleteMovie(create.MediaId, out message);
            }
            else if (create.Type == 2)
            {
                TTvSeries seriesService = new();
                var tvSeries = seriesService.GetAllSeriesOrderedById();

                if (tvSeries == null)
                {
                    return NotFound();
                }

                success = seriesService.TvSeriesDelete(create.MediaId, out message);
            }
            else
            {
                return BadRequest();
            }

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPut]
        //public IActionResult UpdateContent(RequestCreate create)
        //{
        //    if (create == null)
        //    {
        //        return BadRequest();
        //    }

        //    string message;
        //    bool success;

        //    if (create.Type == 1)
        //    {
        //        TMovie movieService = new TMovie();
        //        var movie = new TblMovie()
        //        {
        //            MovieName = create.Title,
        //            MovieDetails = create.Description,
        //            Duration = create.Duration,
        //            ReleaseDate = create.ReleaseDate
        //        };
        //        movie.MovieSeo = movie.MovieName.MCreateSeo();

        //        success = movieService.MovieEdit(create.MediaId, movie, out message);
        //    }
        //    else if (create.Type == 2)
        //    {
        //        TTvSeries seriesService = new TTvSeries();
        //        var series = new TblTvSeries()
        //        {
        //            TvSeriesName = create.Title,
        //            TvSeriesDetails = create.Description,
        //            Duration = create.Duration,
        //            ReleaseDate = create.ReleaseDate
        //        };
        //        series.TvSeriesSeo = series.TvSeriesName.TvCreateSeo();

        //        success = seriesService.TvSeriesEdit(create.MediaId, series, out message);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}



        //[HttpDelete]
        //public IActionResult DeleteMovie(int MovieId)
        //{
        //    MovieSeries movieSeries = new MovieSeries();
        //    TMovie movieService = new();
        //    TTvSeries seriesService = new();

        //    var movie = movieService.GetAllMoviesOrderedById();
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //   string message;
        //   bool success = movieService.DeleteMovie(MovieId, out message);

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpDelete]
        //public IActionResult DeleteSeries(int TvSeriesId)
        //{
        //    MovieSeries movieSeries = new MovieSeries();
        //    TMovie movieService = new();
        //    TTvSeries seriesService = new();

        //    var tvSeries = seriesService.GetAllSeriesOrderedById();
        //      if (tvSeries == null)
        //    {
        //        return NotFound();
        //    }

        //    string message;
        //    bool success = seriesService.TvSeriesDelete(TvSeriesId, out message);

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut]
        //public IActionResult UpdateMovie(int MovieId, TblMovie Movie)
        //{
        //    TMovie movieService = new TMovie();
        //    string message;
        //    bool success = movieService.MovieEdit(MovieId, Movie, out message);

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut]
        //public IActionResult UpdateSeries(int TvSeriesId, TblTvSeries Series)
        //{
        //    TTvSeries seriesService = new();
        //    string message;
        //    bool success = seriesService.TvSeriesEdit(TvSeriesId, Series, out message);

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}



        //[HttpDelete]
        //public IActionResult DeleteMedia(RequestCreate create)
        //{
        //    string message;
        //    bool success;

        //    if (manage.Type == 1) 
        //    {
        //        TMovie movieService = new TMovie();
        //        success = movieService.DeleteMovie(manage.MediaId, out message);
        //    }
        //    else if (manage.Type == 2)
        //    {
        //        TTvSeries seriesService = new TTvSeries();
        //        success = seriesService.TvSeriesDelete(manage.MediaId, out message);
        //    }
        //    else
        //    {
        //        message = "Geçersiz tip!";
        //        success = false;
        //    }

        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
