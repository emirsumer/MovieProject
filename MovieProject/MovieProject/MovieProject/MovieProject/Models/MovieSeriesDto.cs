using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Models
{
    public class MovieSeriesDto
    {
		public List<MoviePhotoVideo> movies { get; set; }
		public List<TvSeriesPhotoVideo> tvSeries { get; set; }
		public List<TblCategory>? tblCategories { get; set; }
		public MovieDetail movieDetail { get; set; }
	}
}
