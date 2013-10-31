using System.Web.Http;
using NoCms.Net.Infrastructure.MVC;

namespace NoCms.Net
{
	public static class NoCmsConfig
	{
		public static void Configure()
		{
			HttpConfiguration config = GlobalConfiguration.Configuration;

			var handler = new CustomHttpControllerDispatcher();

			config.Routes.MapHttpRoute(
				name: "NoCMSWebApi",
				routeTemplate: "nocms/api/{controller}/{id}",
				defaults: new {id = RouteParameter.Optional},
				constraints: null,
				handler: handler);

			config.Routes.MapHttpRoute(
				name: "NoCMSWebApi-Assets",
				routeTemplate: "nocms/assets",
				defaults: new { controller = "Assets" },
				constraints: null,
				handler: handler);
		}
	}
}