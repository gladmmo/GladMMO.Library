using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public sealed class NetworkAvatarContentResourceManager : DefaultLoadableContentResourceManager
	{
		private IClientDataCollectionContainer ClientData { get; }

		private IDownloadableContentServerServiceClient ContentClient { get; }

		public NetworkAvatarContentResourceManager([NotNull] IDownloadableContentServerServiceClient contentClient, 
			ILog logger,
			[NotNull] IClientDataCollectionContainer clientData) 
			: base(logger, UserContentType.Avatar)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
		}

		protected override async Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId)
		{
			if(!ClientData.HasEntry<CreatureDisplayInfoEntry<string>>((int)contentId))
				return new ContentDownloadURLResponse(ContentDownloadURLResponseCode.NoContentId);

			CreatureDisplayInfoEntry<string> displayInfoEntry = ClientData.AssertEntry<CreatureDisplayInfoEntry<string>>((int)contentId);

			if(!ClientData.HasEntry<CreatureModelDataEntry<string>>((int)displayInfoEntry.ModelId))
				return new ContentDownloadURLResponse(ContentDownloadURLResponseCode.NoContentId);

			CreatureModelDataEntry<string> modelDataEntry = ClientData.AssertEntry<CreatureModelDataEntry<string>>(displayInfoEntry.ModelId);

			//WoW content has MDX format in its path, so we should remove it if it exists.
			return new ContentDownloadURLResponse(modelDataEntry.FilePath.Replace(".mdx", ""), 1);
		}
	}
}
