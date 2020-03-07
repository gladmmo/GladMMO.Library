using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncAzureServiceQueueProxiedClient : BaseAsyncEndpointService<IAzureServiceQueueProxiedHttpClient>, IAzureServiceQueueProxiedHttpClient
	{
		/// <inheritdoc />
		public AsyncAzureServiceQueueProxiedClient(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncAzureServiceQueueProxiedClient(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task SendProxiedPostAsync(string jsonBodyContent, string requestPath, string authorizationToken)
		{
			await (await GetService().ConfigureAwaitFalse()).SendProxiedPostAsync(jsonBodyContent, requestPath, authorizationToken).ConfigureAwaitFalseVoid();
		}

		public async Task SendProxiedPatchAsync(string jsonBodyContent, string requestPath, string authorizationToken)
		{
			await (await GetService().ConfigureAwaitFalse()).SendProxiedPatchAsync(jsonBodyContent, requestPath, authorizationToken).ConfigureAwaitFalseVoid();
		}

		public async Task SendProxiedPutAsync(string jsonBodyContent, string requestPath, string authorizationToken)
		{
			await (await GetService().ConfigureAwaitFalse()).SendProxiedPutAsync(jsonBodyContent, requestPath, authorizationToken).ConfigureAwaitFalseVoid();
		}
	}
}