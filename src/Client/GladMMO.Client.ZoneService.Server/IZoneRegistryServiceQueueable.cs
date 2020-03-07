using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: AzureServiceBus")]
	public interface IZoneRegistryServiceQueueable
	{
		[RequiresAuthentication]
		[Patch("/api/ZoneRegistry/checkin")]
		Task ZoneServerCheckinAsync(ZoneServerCheckinRequestModel request);
	}
}
