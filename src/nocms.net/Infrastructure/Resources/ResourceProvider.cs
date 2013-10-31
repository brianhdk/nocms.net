using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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
					.ToDictionary(x => ParseName(x, assemblyName), x => x, StringComparer.OrdinalIgnoreCase);
		}

		private static string ParseName(string resourceName, string assemblyName)
		{
			string name = resourceName.Substring(assemblyName.Length + 1);

			name = Regex.Replace(name, @"\._(?<Digit>\d)", ".${Digit}");

			return name;
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