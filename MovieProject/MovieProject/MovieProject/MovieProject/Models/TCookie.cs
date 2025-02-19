using Microsoft.Net.Http.Headers;

namespace MovieProject.Models
{
	public class TCookie
	{
		public bool CreateCookie(
			HttpRequest request,
			HttpResponse response,
			string Name,
			string Value,
			int Minute)
		{
			bool result = false;
			try
			{
				var resp = new HttpResponseMessage();

				var cookie = new CookieHeaderValue("session-id", "12345");
				//cookie.Expires = DateTimeOffset.Now.AddDays(1);
				//cookie.Domain = Request.RequestUri.Host;
				//cookie.Path = "/";

				//resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });


				CookieOptions options = new CookieOptions();
				options.Domain = request.Host.Value;
				options.Expires = DateTimeOffset.Now.AddMinutes(20);
				options.Secure = true;
				options.HttpOnly = true;
				response.Cookies.Append(Name, Value, options);
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}
		public bool GetCookie(HttpRequest request, string Name, out string Value)
		{
			bool result = false;
			Value = string.Empty;
			try
			{
				if (request.Cookies.Count > 0)
				{
					var CookieValue = request.Cookies[Name];
					if (CookieValue != null)
					{
						Value = CookieValue.ToString();
						result = true;
					}
				}
			}
			catch (Exception)
			{

			}
			return result;
		}
	}
}
