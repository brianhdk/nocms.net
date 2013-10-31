using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NoCms.Net.Hubs;

namespace NoCms.Net.Infrastructure.OWIN
{
	internal class DependencyResolver : DefaultDependencyResolver
	{
		public DependencyResolver()
		{
			OverrideServices();
		}

		private void OverrideServices()
		{
			Register(typeof (IHubDescriptorProvider), () => new HubDescriptorProvider());
		}
	}

	internal class HubDescriptorProvider : IHubDescriptorProvider
	{
		private static readonly Lazy<List<HubDescriptor>> Hubs = new Lazy<List<HubDescriptor>>(InitializeHubs);

		private static List<HubDescriptor> InitializeHubs()
		{
			return
				typeof(HubDescriptorProvider).Assembly
				.GetTypes()
				.Where(x => String.Equals(x.Namespace, typeof(ChatHub).Namespace))
				.Where(x => x.Name.EndsWith("Hub"))
				.Select(x => new HubDescriptor
				{
					NameSpecified = false,
					Name = x.Name,
					HubType = x
				})
				.ToList();
		}

		public IList<HubDescriptor> GetHubs()
		{
			return Hubs.Value;
		}

		public bool TryGetHub(string hubName, out HubDescriptor descriptor)
		{
			descriptor = Hubs.Value.SingleOrDefault(x => String.Equals(x.Name, hubName, StringComparison.OrdinalIgnoreCase));

			return descriptor != null;
		}
	}
}