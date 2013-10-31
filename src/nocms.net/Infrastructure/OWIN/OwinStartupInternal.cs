using Microsoft.Owin;
using NoCms.Net.Infrastructure.OWIN;
using Owin;

//[assembly: OwinStartup(typeof (OwinStartupInternal), "Configuration")]

namespace NoCms.Net.Infrastructure.OWIN
{
	internal class OwinStartupInternal
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseNoCms();
		}
	}
}