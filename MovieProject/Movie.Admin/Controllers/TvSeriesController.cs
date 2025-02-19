using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
    public class TvSeriesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            MovieSeriesDto movieSeries = new();
            TTvSeries tvseries = new TTvSeries();


            movieSeries.tvSeries = tvseries.TvSeriesList();
            return View(movieSeries);
        }
    }
}
