using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncVivoxAuthorizationServiceClient : BaseAsyncEndpointService<IVivoxAuthorizationService>, IVivoxAuthorizationService
	{
		/// <inheritdoc />
		public AsyncVivoxAuthorizationServiceClient(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncVivoxAuthorizationServiceClient(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<string, VivoxLoginResponseCode>> LoginAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).LoginAsync().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<VivoxChannelJoinResponse, VivoxLoginResponseCode>> JoinProximityChatAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).JoinProximityChatAsync().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<VivoxChannelJoinResponse, VivoxLoginResponseCode>> JoinGuildChatAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).JoinGuildChatAsync().ConfigureAwaitFalse();
		}
	}
}
