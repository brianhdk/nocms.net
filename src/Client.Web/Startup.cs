using Client.Web;
using Microsoft.Owin;
using Owin;
using NoCms.Net.Infrastructure.OWIN;

[assembly: OwinStartup(typeof(Startup), "Configuration")]

namespace Client.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();

			app.UseNoCms();
		}
	}
}