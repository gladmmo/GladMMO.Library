using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: AzureServiceBus")]
	public interface IZonePersistenceServiceQueueable
	{
		[RequiresAuthentication]
		[Patch("/api/ZonePersistence/{id}/data")]
		Task SaveFullCharacterDataAsync([AliasAs("id")] int characterId, [JsonBody] FullCharacterDataSaveRequest saveRequest);

		[RequiresAuthentication]
		[Patch("/api/ZonePersistence/{id}/location")]
		Task SaveCharacterLocation([AliasAs("id")] int characterId, [JsonBody] ZoneServerCharacterLocationSaveRequest saveRequest);
	}
}
