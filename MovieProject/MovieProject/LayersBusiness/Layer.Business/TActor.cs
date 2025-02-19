using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Layer.Business
{
	public class TActor : DbProcess, IDatabase
	{
		public TActor()
		{
			this.Context = new DbMovieEntities();
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

		public bool ActorAdd(ref TblActor Actor, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblActor.Add(Actor);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool ActorsAdd(ref List<TblActor> Actors, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblActor.AddRange(Actors);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool ActorEdit(int ActorId, TblActor Actor, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				var ActorData = (from data in Context.TblActor
								 where data.ActorId == Actor.ActorId
								 select data).FirstOrDefault();
				if (ActorData != null)
				{
					ActorData = Actor;

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

		public bool ActorDelete(int ActorId, out string Mesaj)
		{
			bool result = false;
			Mesaj = string.Empty;
			try
			{
				Context.TblActor.Remove(new DataLayer.TblActor()
				{ ActorId = ActorId });
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public ActorDetails GetActorDetail(int ActorId)
		{
			ActorDetails result = new ActorDetails();

			var ActorDetail = (from data in Context.TblActor
							   where data.ActorId == ActorId
							   select data).FirstOrDefault();

			if (ActorDetail != null)
			{
				result.Actor = ActorDetail;

				var ActorMovies = (from data in Context.TblMovie
									where data.MovieId == ActorId
									select data).ToList();
				result.Movies =ActorMovies;

				var ActorTvSeries = (from data in Context.TblTvSeries
								where data.TvSeriesId == ActorId
								select data).ToList();
				result.TvSeries=ActorTvSeries;
			}
			return result;
		}

    	public List<TblActor> ActorList()
	    {
			return (from data in Context.TblActor
					orderby data.ActorName
					select data).AsNoTracking().ToList();
	    }

		public List<TblActor> MovieActorList(int movieId)
		{
			return (from actor in Context.TblActor
					where actor.MovieId == movieId  
					orderby actor.ActorId 
					select actor).AsNoTracking().ToList();
		}

		public List<TblActor> TvSeriesActorList(int TvSeriesId)
		{
			return (from actor in Context.TblActor
					where actor.TvSeriesId == TvSeriesId
					orderby actor.ActorId
					select actor).AsNoTracking().ToList();
		}

	}
}

