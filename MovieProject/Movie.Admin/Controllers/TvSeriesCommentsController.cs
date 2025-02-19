using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
	public class TvSeriesCommentsController : Controller
	{
		public IActionResult Index()
		{
			MovieSeries movieSeries = new();
			TTvSeriesComment SComment = new();
			movieSeries.SeriesComment = SComment.GetSeriesCommentNew();

			return View(movieSeries);
		}
	}
}
