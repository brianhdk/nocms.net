using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace NoCms.Net.Infrastructure.MVC
{
	internal class AssembliesResolver : IAssembliesResolver
	{
		public ICollection<Assembly> GetAssemblies()
		{
			return new[] {GetType().Assembly};
		}
	}
}