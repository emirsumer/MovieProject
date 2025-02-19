using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Business
{
	public static class TMovieStringExtension
	{
		public static string MCreateSeo(this string MovieName)
		{
			string Seo = MovieName.ToLower();
			Seo = Seo.Replace("ğ", "g");
			Seo = Seo.Replace("ı", "i");
			Seo = Seo.Replace("ç", "c");
			Seo = Seo.Replace("ş", "s");
			Seo = Seo.Replace("ö", "o");
			Seo = Seo.Replace("ü", "u");
			Seo = Seo.Replace(" ", "-");
			Seo = Seo.Replace("/", "");
			Seo = Seo.Replace("//", "");
			Seo = Seo.Replace("*", "");
			Seo = Seo.Replace(".", "");
			Seo = Seo.Replace("_", "");
			Seo = Seo.Replace("?", "");
			Seo = Seo.Replace(",", "");
			Seo = Seo.Replace("=", "");
			Seo = Seo.Replace("{", "");
			Seo = Seo.Replace("}", "");
			Seo = Seo.Replace("[", "");
			Seo = Seo.Replace("]", "");
			Seo = Seo.Replace("!", "");
			Seo = Seo.Replace("'", "");
			Seo = Seo.Replace("%", "");
			Seo = Seo.Replace("+", "");
			Seo = Seo.Replace("€", "");
			Seo = Seo.Replace("&", "");
			Seo = Seo.Replace("(", "");
			Seo = Seo.Replace(")", "");
			return Seo;
		}
	}
}
