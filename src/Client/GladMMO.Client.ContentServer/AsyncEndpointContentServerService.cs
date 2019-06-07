using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointContentServerService : BaseAsyncEndpointService<IContentServerServiceClient>, IContentServerServiceClient
	{
		/// <inheritdoc />
		public AsyncEndpointContentServerService(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncEndpointContentServerService(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<RequestedUrlResponseModel> GetNewWorldUploadUrl(string authToken)
		{
			return await (await GetService().ConfigureAwait(false)).GetNewWorldUploadUrl(authToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<RequestedUrlResponseModel> GetNewAvatarUploadUrl(string authToken)
		{
			return await (await GetService().ConfigureAwait(false)).GetNewAvatarUploadUrl(authToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestWorldDownloadUrl(long worldId, string authToken)
		{
			return await (await GetService().ConfigureAwait(false)).RequestWorldDownloadUrl(worldId, authToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestAvatarDownloadUrl(long avatarId, string authToken)
		{
			return await (await GetService().ConfigureAwait(false)).RequestAvatarDownloadUrl(avatarId, authToken).ConfigureAwait(false);
		}
	}
}
