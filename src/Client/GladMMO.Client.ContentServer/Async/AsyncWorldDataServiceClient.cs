using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncWorldDataServiceClient : BaseAsyncEndpointService<IWorldDataServiceClient>, IWorldDataServiceClient
	{
		/// <inheritdoc />
		public AsyncWorldDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncWorldDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<bool> CheckWorldExistsAsync(int worldId)
		{
			return await (await GetService().ConfigureAwaitFalse()).CheckWorldExistsAsync(worldId).ConfigureAwaitFalse();
		}
	}
}
