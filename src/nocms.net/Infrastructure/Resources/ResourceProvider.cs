using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NoCms.Net.Infrastructure.Resources
{
	public class ResourceProvider
	{
		private static readonly Assembly Assembly = typeof (ResourceProvider).Assembly;

		private static readonly Lazy<IDictionary<string, string>> ResourceNames = new Lazy<IDictionary<string, string>>(InitializeResourceNames);

		private static IDictionary<string, string> InitializeResourceNames()
		{
			string assemblyName = Assembly.GetName().Name;

			return 
				Assembly.GetManifestResourceNames()
					.ToDictionary(x => x.Substring(assemblyName.Length + 1), x => x, StringComparer.OrdinalIgnoreCase);
		}

		public Stream GetResourceStream(string file)
		{
			string resourceName = file.Replace("/", ".");

			string resourceNameKey;
			if (ResourceNames.Value.TryGetValue(resourceName, out resourceNameKey))
				return Assembly.GetManifestResourceStream(resourceNameKey);

			return null;
		}
	}
}