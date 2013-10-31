using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dispatcher;
using nocms.net.Controllers;

namespace NoCms.Net.Infrastructure.MVC
{
	internal class HttpControllerTypeResolver : IHttpControllerTypeResolver
	{
		public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			return
				GetType().Assembly
				         .GetTypes()
				         .Where(x => String.Equals(x.Namespace, typeof (ModelController).Namespace))
				         .Where(x => x.Name.EndsWith("Controller"))
				         .ToList();
		}
	}
}