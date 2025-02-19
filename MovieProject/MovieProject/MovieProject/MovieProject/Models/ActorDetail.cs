using DataLayer;

namespace MovieProject.Models
{
    public class ActorDetail
    {
        public TblActor Actor { get; set; }
		public List<ActorDetails> actors { get; set; }
		public TblMovie Movies { get; set; }
        public TblTvSeries TvSeries { get; set; }

    }
}
