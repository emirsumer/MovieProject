using DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public class TCategory : DbProcess, IDatabase
	{
		public TCategory()
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

		public bool CategoryAdd(TblCategory Category, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				Context.TblCategory.Add(Category);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool CategoryAdd(List<TblCategory> Categories, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				Context.TblCategory.AddRange(Categories);
				Context.SaveChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public bool CategoryEdit(int CategoryId, TblCategory Category, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{
				var FindCategory = (from data in
										   TblCategory
									where
									   data.CategoryId == CategoryId
									select data).
									   FirstOrDefault();
				if (FindCategory != null)
				{
					FindCategory = Category;
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

		public bool CategoryDelete(int CategoryId, out string Mesaj)
		{
			bool result = false;
			Mesaj = "";
			try
			{

				Context.Database.ExecuteSqlCommand("delete from Category.TblCategory " + "where CategoryId=" + CategoryId);
				result = true;

			}
			catch (Exception ex)
			{
				Mesaj = ex.Message;
			}
			return result;
		}

		public List<TblCategory> CategoryList()
		{
			return (from data in Context.TblCategory orderby data.CategoryName select data).AsNoTracking().ToList();
		}

		public List<TblCategory> MovieCategoryList(TblMovie Movie)
		{
			return (from data in Context.TblCategory where data.TblMovie.Contains(Movie) orderby data.CategoryName select data).AsNoTracking().ToList();
		}

		public IQueryable<TblCategory> TvSeriesCategoryList(TblTvSeries TvSeries)
		{
			return (from data in Context.TblCategory where data.TblTvSeries.Contains(TvSeries) orderby data.CategoryName select data).AsNoTracking().AsQueryable();
		}

	}
}
