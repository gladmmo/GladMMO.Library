using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Proxy interface for ServerSelection (List) Server RPCs.
	/// </summary>
	[Headers("User-Agent: GuardiansClient")]
	public interface IServerSelectionService
	{
		/// <summary>
		/// Gets all gameservers listed without
		/// any filtering.
		/// </summary>
		/// <returns>All known gameservers from the service.</returns>
		[Headers("Cache-Control: max-age=60")]
		[Get("/api/gameservers/all")]
		Task<GameServerListResponseModel> GetServers();
		
		//TODO: We can add filtering requests on flags later
	}
}
