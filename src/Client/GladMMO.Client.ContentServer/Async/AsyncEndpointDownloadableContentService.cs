using System; using FreecraftCore;
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
			return await (await GetService().ConfigureAwaitFalse()).GetNewWorldUploadUrl().ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingWorld(long worldId)
		{
			return await(await GetService().ConfigureAwaitFalse()).RequestUpdateExistingWorld(worldId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewAvatarUploadUrl()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetNewAvatarUploadUrl().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingAvatar(long avatarId)
		{
			return await(await GetService().ConfigureAwaitFalse()).RequestUpdateExistingAvatar(avatarId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewCreatureModelUploadUrl()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetNewCreatureModelUploadUrl().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingCreature(long creatureModelId)
		{
			return await(await GetService().ConfigureAwaitFalse()).RequestUpdateExistingCreature(creatureModelId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestWorldDownloadUrl(long worldId)
		{
			return await (await GetService().ConfigureAwaitFalse()).RequestWorldDownloadUrl(worldId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ContentDownloadURLResponse> RequestAvatarDownloadUrl(long avatarId)
		{
			return await (await GetService().ConfigureAwaitFalse()).RequestAvatarDownloadUrl(avatarId).ConfigureAwaitFalse();
		}

		public async Task<ContentDownloadURLResponse> RequestCreatureModelDownloadUrl(long creatureId)
		{
			return await (await GetService().ConfigureAwaitFalse()).RequestCreatureModelDownloadUrl(creatureId).ConfigureAwaitFalse();
		}

		public async Task<ContentDownloadURLResponse> RequestGameObjectModelDownloadUrl(long gameObjectModelId)
		{
			return await (await GetService().ConfigureAwaitFalse()).RequestGameObjectModelDownloadUrl(gameObjectModelId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewGameObjectModelUploadUrl()
		{
			return await(await GetService().ConfigureAwaitFalse()).GetNewGameObjectModelUploadUrl().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingGameObjectModel(long gameObjectModelId)
		{
			return await (await GetService().ConfigureAwaitFalse()).RequestUpdateExistingGameObjectModel(gameObjectModelId).ConfigureAwaitFalse();
		}
	}
}
