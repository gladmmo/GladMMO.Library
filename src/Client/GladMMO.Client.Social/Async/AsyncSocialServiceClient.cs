using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncSocialServiceClient : BaseAsyncEndpointService<ISocialService>, ISocialService
	{
		/// <inheritdoc />
		public AsyncSocialServiceClient(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncSocialServiceClient(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<CharacterFriendListResponseModel> GetCharacterListAsync()
		{
			return await (await GetService().ConfigureAwait(false)).GetCharacterListAsync().ConfigureAwait(false);
		}
	}
}
