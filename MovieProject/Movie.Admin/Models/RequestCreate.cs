using DataLayer;

namespace Movie.Admin.Models
{
	public class RequestCreate
	{
        public string Title { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
		public int ReleaseDate { get; set; }
		public int Type { get; set; }
        public int MediaId { get; set; }
        public string Message { get; set; }

        public List<TblMovie> movies { get; set; }
        public List<TblTvSeries> tvSeries { get; set; }
		public List<TblMovieComment> moviesComment { get; set; }
		public List<TblTvSeriesComment> tvSeriesComment { get; set; }

    }
}
