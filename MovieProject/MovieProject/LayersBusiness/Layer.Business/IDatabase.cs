using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public interface IDatabase
	{
		DbMovieEntities Context { get; set; }
		void DbConnect();
		void DbDisconnect();
	}
}
