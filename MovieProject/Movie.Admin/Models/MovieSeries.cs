using DataLayer;

namespace Movie.Admin.Models
{
    public class MovieSeries
    {
        public List<TblMovie> movies { get; set; }
        public List<TblMovie> Movies { get; set; }


        public List<TblTvSeries> tvSeries { get; set; }
        public List<TblTvSeries> TvSeries { get; set; }

        public List<TblCategory>? Categories { get; set; }
        public List<TblKullanici> kullanicilar { get; set; }


        public List<TblMovieComment> moviesComments { get; set; }
        public List<TblMovieComment> MoviesComments { get; set; }


        public List<TblTvSeriesComment> seriesComment { get; set; }
        public List<TblTvSeriesComment> SeriesComment { get; set; }


    }
}
