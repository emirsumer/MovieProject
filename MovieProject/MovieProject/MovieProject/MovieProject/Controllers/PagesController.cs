using Microsoft.AspNetCore.Mvc;
using DataLayer;
using Layer.Business;
using MovieProject.Models;


namespace MovieProject.Controllers
{
	public class PagesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult HelpCenter()
		{
			return View();
		}
		public IActionResult Profile()
		{
			return View();
		}
	

		//public IActionResult Actor(string id)
		//{
		//	TActor actor = new();
		//	var ActorDetail = actor.GetActorDetail(Convert.ToInt32(id));

		//	if (ActorDetail == null || ActorDetail.Actor == null)
		//	{
		//		return NotFound();
		//	}

		//	MovieProject.Models.ActorDetail actorDetailDto = new MovieProject.Models.ActorDetail
		//	{
		//		Actor = ActorDetail.Actor,
		//		TvSeries = ActorDetail.TvSeries,
		//		Movies = ActorDetail.Movies
		//	};

		//	return View(actorDetailDto);
		//}
		   public IActionResult Actor(string id)
		   {
			TActor actor = new();
		    var ActorDetails = actor.GetActorDetail(Convert.ToInt32(id));

			ActorDetail actorDetailDto = new();
		    actorDetailDto.Actor = ActorDetails.Actor;
			actorDetailDto.TvSeries = ActorDetails.Actor.TblTvSeries;
			actorDetailDto.Movies = ActorDetails.Actor.TblMovie;

			if (actorDetailDto != null)
		    {
				return View(actorDetailDto);
			}
			else
			{
				return View("~/Error/Index");
		    }
		   }
		public IActionResult Contacts()
		{
			return View();
		}
		public IActionResult PrivacyPolicy()
		{
			return View();
		}
     
    }
}
