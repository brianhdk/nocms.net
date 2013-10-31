using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace NoCms.Net.Infrastructure.MVC
{
	internal class CustomHttpControllerDispatcher : HttpControllerDispatcher
	{
		public CustomHttpControllerDispatcher()
			: base(CreateConfiguration())
		{
		}

		private static HttpConfiguration CreateConfiguration()
		{
			var config = new HttpConfiguration();

			config.Services.Replace(typeof (IAssembliesResolver), new AssembliesResolver());
			config.Services.Replace(typeof (IHttpControllerTypeResolver), new HttpControllerTypeResolver());

			return config;
		}
	}
}