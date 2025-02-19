using DataLayer;

namespace Movie.Admin.Models
{
    public class MovieSeriesDto
    {
        public List<MoviePhotoVideo> movies { get; set; }
        public List<TvSeriesPhotoVideo> tvSeries { get; set; }
        public List<TblCategory>? tblCategories { get; set; }
    }
}
