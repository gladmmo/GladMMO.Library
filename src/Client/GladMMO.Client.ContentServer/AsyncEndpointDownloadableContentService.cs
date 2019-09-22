using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointDownloadableContentService : BaseAsyncEndpointService<IDownloadableContentServerServiceClient>, IDownloadableContentServerServiceClient
	{
		/// <inheritdoc />
		public AsyncEndpointDownloadableContentService(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncEndpointDownloadableContentService(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		/// <inheritdoc />
		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewWorldUploadUrl()
		{
			return await (await GetService().ConfigureAwait(false)).GetNewWorldUploadUrl().ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewAvatarUploadUrl()
		{
			return await (await GetService().ConfigureAwait(false)).GetNewAvatarUploadUrl().ConfigureAwait(false);
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewCreatureModelUploadUrl()
		{
			return await (await GetService().ConfigureAwait(false)).GetNewCreatureModelUploadUrl().ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestWorldDownloadUrl(long worldId)
		{
			return await (await GetService().ConfigureAwait(false)).RequestWorldDownloadUrl(worldId).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestAvatarDownloadUrl(long avatarId)
		{
			return await (await GetService().ConfigureAwait(false)).RequestAvatarDownloadUrl(avatarId).ConfigureAwait(false);
		}

		public async Task<ContentDownloadURLResponse> RequestCreatureModelDownloadUrl(long creatureId)
		{
			return await (await GetService().ConfigureAwait(false)).RequestCreatureModelDownloadUrl(creatureId).ConfigureAwait(false);
		}
	}
}
