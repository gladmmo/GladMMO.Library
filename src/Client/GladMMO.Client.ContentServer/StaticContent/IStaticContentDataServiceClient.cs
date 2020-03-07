using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IStaticContentDataServiceClient
	{
		//TODO: Enable caching for production.
		//[Headers("Cache-Control: max-age=5000")]
		[Get("/api/ContentIcon")]
		Task<ContentIconInstanceModel[]> ContentIconsAsync();
	}
}
