using System;
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
		[Post("/api/ZoneRegistry/checkin")]
		Task ZoneServerCheckinAsync();
	}
}
