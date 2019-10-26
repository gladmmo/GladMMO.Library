using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public class AsyncEndpointISocialServiceToGameServiceClient : BaseAsyncEndpointService<ISocialServiceToGameServiceClient>, ISocialServiceToGameServiceClient
	{
		/// <inheritdoc />
		public AsyncEndpointISocialServiceToGameServiceClient([JetBrains.Annotations.NotNull] Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncEndpointISocialServiceToGameServiceClient([JetBrains.Annotations.NotNull] Task<string> futureEndpoint, [JetBrains.Annotations.NotNull] RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<CharacterSessionDataResponse> GetCharacterSessionDataByAccount(int accountId)
		{
			return await ((await GetService().ConfigureAwait(false)).GetCharacterSessionDataByAccount(accountId).ConfigureAwait(false));
		}
	}
}
