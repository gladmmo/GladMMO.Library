using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class UpdatedContentUploadTokenFactory : IContentUploadTokenFactory
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		private WorldDefinitionData WorldData { get; }

		public UpdatedContentUploadTokenFactory([NotNull] IDownloadableContentServerServiceClient contentClient, [NotNull] WorldDefinitionData worldData)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
			WorldData = worldData ?? throw new ArgumentNullException(nameof(worldData));
		}

		public Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> Create(ContentUploadTokenFactoryContext context)
		{
			switch (context.ContentType)
			{
				case UserContentType.World:
					return ContentClient.RequestUpdateExistingWorld(WorldData.WorldId);
				case UserContentType.Avatar:
					throw new NotImplementedException($"TODO: Implement avatar updating.");
				case UserContentType.Creature:
					throw new NotImplementedException($"TODO: Implement avatar updating.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
