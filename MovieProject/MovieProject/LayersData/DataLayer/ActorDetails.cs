using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class ActorDetails
	{
		public TblActor Actor { get; set; }
		public List<TblMovie> Movies { get; set; }
		public List<TblTvSeries> TvSeries { get; set; }

	}
}
