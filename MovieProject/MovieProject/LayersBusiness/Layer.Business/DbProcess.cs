using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Layer.Business
{
	public class DbProcess : DbMovieEntities
	{
		public DbMovieEntities Context { get; set; }
		public void ConnectDb()
		{
			if (this.Database.Connection.State == System.Data.ConnectionState.Closed)
				this.Database.Connection.Open();
		}

		public void DisconnectDb()
		{
			if (this.Database.Connection.State != System.Data.ConnectionState.Closed)
				this.Database.Connection.Close();
		}
	}
}