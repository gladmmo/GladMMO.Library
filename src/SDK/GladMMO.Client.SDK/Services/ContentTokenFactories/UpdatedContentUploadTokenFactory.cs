using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO.SDK
{
	public sealed class UpdatedContentUploadTokenFactory : IContentUploadTokenFactory
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		private IUploadedContentDataDefinition ContentDataDefinition { get; }

		public UpdatedContentUploadTokenFactory([NotNull] IDownloadableContentServerServiceClient contentClient, [NotNull] IUploadedContentDataDefinition contentDataDefinition)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
			ContentDataDefinition = contentDataDefinition ?? throw new ArgumentNullException(nameof(contentDataDefinition));
		}

		public Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> Create(ContentUploadTokenFactoryContext context)
		{
			switch (context.ContentType)
			{
				case UserContentType.World:
					return ContentClient.RequestUpdateExistingWorld(ContentDataDefinition.ContentId);
				case UserContentType.Avatar:
					return ContentClient.RequestUpdateExistingAvatar(ContentDataDefinition.ContentId);
				case UserContentType.Creature:
					throw new NotImplementedException($"TODO: Implement avatar updating.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
