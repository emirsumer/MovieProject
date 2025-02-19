using DataLayer;

namespace MovieProject.Models
{
    public class TvSeriesDetail
    {
		public TblTvSeries TvSeries { get; set; }
		public string TvSeriesOpeningImage { get; set; }
		public string TvSeriesVideo { get; set; }
		public string BannerUrl { get; set; }
		public string VideoUrl { get; set; }
		public List<TblTvSeries> TvSeriess { get; set; } = new List<TblTvSeries>();
		public List<TvSeriesPhotoVideo> AllTvSeries { get; set; } = new List<TvSeriesPhotoVideo>();
		public List<TblTvSeriesPhoto> TvSeriesPhotos { get; set; } = new List<TblTvSeriesPhoto>();
		public List<TblTvSeriesVideo> TvSeriesVideos { get; set; } = new List<TblTvSeriesVideo>();
		public TblTvSeriesComment TComment { get; set; }
		public List<TblTvSeriesComment> SeriesComments { get; set;}
	}
}
