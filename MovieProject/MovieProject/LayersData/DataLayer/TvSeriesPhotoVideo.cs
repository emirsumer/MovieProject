using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class TvSeriesPhotoVideo
	{
		public TblTvSeries TvSeries { get; set; }
		public string TSOpeningImage { get; set; }
		public string TSVideo { get; set; }
		public List<TblTvSeries> TvSeriess { get; set; } = new List<TblTvSeries>();
		public List<TblTvSeriesPhoto> TvSeriesPhotos { get; set; } = new List<TblTvSeriesPhoto>();
		public List<TblTvSeriesVideo> TvSeriesVideos { get; set; } = new List<TblTvSeriesVideo>();
        public int CommentCount { get; set; }

    }
}
