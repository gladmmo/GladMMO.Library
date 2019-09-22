using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class NewContentUploadTokenFactory : IContentUploadTokenFactory
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		public NewContentUploadTokenFactory([NotNull] IDownloadableContentServerServiceClient contentClient)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
		}

		public Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> Create(ContentUploadTokenFactoryContext context)
		{
			switch (context.ContentType)
			{
				case UserContentType.World:
					return ContentClient.GetNewWorldUploadUrl();
				case UserContentType.Avatar:
					return ContentClient.GetNewAvatarUploadUrl();
				case UserContentType.Creature:
					return ContentClient.GetNewCreatureModelUploadUrl();
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
