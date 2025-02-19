using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
    public class TDirector : DbProcess, IDatabase
    {
        public TDirector()
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
    }
}
