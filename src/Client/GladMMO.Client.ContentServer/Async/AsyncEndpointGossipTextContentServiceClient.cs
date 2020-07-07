using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointGossipTextContentServiceClient : BaseAsyncEndpointService<IGossipTextContentServiceClient>, IGossipTextContentServiceClient
	{
		/// <inheritdoc />
		public AsyncEndpointGossipTextContentServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncEndpointGossipTextContentServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<string> GetCreatureGossipTextAsync(int textId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCreatureGossipTextAsync(textId).ConfigureAwaitFalse();
		}
	}
}
