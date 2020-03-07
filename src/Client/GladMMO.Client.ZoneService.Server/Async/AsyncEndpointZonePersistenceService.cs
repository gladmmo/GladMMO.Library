using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointZonePersistenceService : BaseAsyncEndpointService<IZonePersistenceServiceQueueable>, IZonePersistenceServiceQueueable
	{
		/// <inheritdoc />
		public AsyncEndpointZonePersistenceService(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointZonePersistenceService(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task SaveFullCharacterDataAsync(int characterId, FullCharacterDataSaveRequest saveRequest)
		{
			await (await GetService().ConfigureAwaitFalse()).SaveFullCharacterDataAsync(characterId, saveRequest).ConfigureAwaitFalseVoid();
		}

		public async Task SaveCharacterLocation(int characterId, ZoneServerCharacterLocationSaveRequest saveRequest)
		{
			await (await GetService().ConfigureAwaitFalse()).SaveCharacterLocation(characterId, saveRequest).ConfigureAwaitFalseVoid();
		}
	}
}