using DataLayer;

namespace MovieProject.Models
{
    public class MovieDetail
    {
		public TblMovie Moviess { get; set; }
        public TblActor Actor { get; set; }
        public List<ActorDetails> actors { get; set; }
        public string MovieOpeningImage { get; set; }
		public string MovieVideo { get; set; }
		public string BannerUrl { get; set; }
		public string VideoUrl { get; set; }
		public List<MoviePhotoVideo> Movies { get; set; } = new List<MoviePhotoVideo>();
		public List<TblMoviePhoto> MoviePhotos { get; set; } = new List<TblMoviePhoto>();
		public List<TblMovieVideo> MovieVideos { get; set; } = new List<TblMovieVideo>();
		public List<TblMovieComment> movieComments { get; set; }
        public TblMovieComment MComment { get; set; }
		public List<TblKisi> kisi { get; set; }


    }
}