using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IWorldDataServiceClient
	{
		[Get("/api/World/{id}/exists")]
		Task<bool> CheckWorldExistsAsync([AliasAs("id")] int worldId);
	}
}