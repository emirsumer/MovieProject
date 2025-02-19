using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataLayer;
using Layer.Business;
using MovieProject.Models;

namespace MovieProject.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			MovieSeriesDto movieSeriesDto = new MovieSeriesDto();

			TTvSeries tvseries = new TTvSeries();
			TMovie movie = new TMovie();
		
            movieSeriesDto.movies = movie.LastAddMovieList(5);
			movieSeriesDto.tvSeries = tvseries.LastAddTvSeriesList(5);

            return View(movieSeriesDto);
		}
     
    }
}
