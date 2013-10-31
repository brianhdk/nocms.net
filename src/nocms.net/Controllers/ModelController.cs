using System.Collections.Generic;
using System.Web.Http;

namespace nocms.net.Controllers
{
	internal class ModelController : ApiController
	{
		// GET api/api
		public IEnumerable<string> Get()
		{
			return new string[] { "no-cms-value1", "no-cms-value2" };
		}

		// GET api/api/5
		public string Get(int id)
		{
			return "no-cms-value";
		}

		// POST api/api
		public void Post([FromBody]string value)
		{
		}

		// PUT api/api/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/api/5
		public void Delete(int id)
		{
		}
	}
}