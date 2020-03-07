using System; using FreecraftCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GladMMO
{
	//Queryable endpoint for browser-level online status testing.
	[Route("api/[controller]")]
	public class HelloController : Controller
	{
		[ResponseCache(Duration = int.MaxValue)]
		[HttpGet]
		public Task<string> Hello()
		{
			return Task.FromResult("Hello");
		}
	}
}
