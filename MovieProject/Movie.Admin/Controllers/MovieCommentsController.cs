using DataLayer;
using Layer.Business;
using Microsoft.AspNetCore.Mvc;
using Movie.Admin.Models;

namespace Movie.Admin.Controllers
{
	public class MovieCommentsController : Controller
	{
		public IActionResult Index()
		{
			MovieSeries movieSeries = new();
			TMovieComment MComment = new();
			movieSeries.moviesComments = MComment.GetMovieCommentNew();
			return View(movieSeries);
		}
	}
}
