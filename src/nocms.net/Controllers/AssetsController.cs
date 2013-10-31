using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using NoCms.Net.Infrastructure.Resources;

namespace nocms.net.Controllers
{
	public class AssetsController : ApiController
	{
		private static readonly Regex FileNameValidator = 
			new Regex(@"^[^./~]+.*\..+$", RegexOptions.IgnoreCase);

		private static readonly IDictionary<string, string> MediaTypes =
			new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{".js", "application/javascript"},
			{".html", "text/html"},
			{".htm", "text/html"},
			{".css", "text/css"},
			{".svg", "image/svg+xml"},
			{".eot", "application/vnd.ms-fontobject"},
			{".ttf", "application/x-font-ttf"},
			{".woff", "application/x-woff"},
			{".jpg", "image/jpeg"},
			{".png", "image/png"},
			{".gif", "image/gif"}
		};

		private static readonly ResourceProvider ResourceProvider = new ResourceProvider();
		
		public HttpResponseMessage Get(string file)
		{
			// todo: look into Request.CreateErrorResponse
			if (String.IsNullOrWhiteSpace(file))
				throw new HttpException((int) HttpStatusCode.BadRequest, "File is missing.");

			if (!FileNameValidator.IsMatch(file))
				throw new HttpException((int) HttpStatusCode.BadRequest, "File is invalid.");

			string extension = Path.GetExtension(file);

			string mediaType;
			if (!MediaTypes.TryGetValue(extension, out mediaType))
				throw new HttpException((int) HttpStatusCode.BadRequest, "File type is not supported.");

			var stream = ResourceProvider.GetResourceStream(file);

			if (stream == null)
				throw new HttpException((int) HttpStatusCode.NotFound, "File not found.");

			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(stream)
			};

			result.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

			return result;
		}
	}
}