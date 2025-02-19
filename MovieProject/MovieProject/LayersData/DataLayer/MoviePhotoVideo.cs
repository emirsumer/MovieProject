using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class MoviePhotoVideo
	{
		public TblMovie Movie { get; set; }
		public string MovieOpeningImage { get; set; }
		public string MovieVideo { get; set; }
		public List<TblMovie> Movies { get; set; } = new List<TblMovie>();
		public List<TblMoviePhoto> MoviePhotos { get; set; } = new List<TblMoviePhoto>();
		public List<TblMovieVideo> MovieVideos { get; set; } = new List<TblMovieVideo>();
        public int CommentCount { get; set; }
    }
}
