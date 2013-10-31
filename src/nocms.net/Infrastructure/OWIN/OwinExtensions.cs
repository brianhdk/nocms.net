using System;
using Microsoft.AspNet.SignalR;
using Owin;

namespace NoCms.Net.Infrastructure.OWIN
{
	public static class OwinExtensions
	{
		public static void UseNoCms(this IAppBuilder app)
		{
			if (app == null) throw new ArgumentNullException("app");

			app.MapSignalR("/nocms/signalr", new HubConfiguration
			{
				Resolver = new DependencyResolver()
			});
		}
	}
}