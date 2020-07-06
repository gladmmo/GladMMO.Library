using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public class GaiaPlayerAvatarContentResourceManager : NetworkAvatarContentResourceManager
	{
		private IClientDataCollectionContainer ClientData { get; }

		public GaiaPlayerAvatarContentResourceManager([NotNull] IDownloadableContentServerServiceClient contentClient, 
			ILog logger,
			[NotNull] IClientDataCollectionContainer clientData) 
			: base(contentClient, logger, clientData)
		{
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

			//TODO: Update FLAGS, I think this is PLAYABLE race.
			if (((int) modelDataEntry.Flags & 2048) != 0)
			{
				//WoW content has MDX format in its path, so we should remove it if it exists.
				//return new ContentDownloadURLResponse(modelDataEntry.FilePath.Replace(".mdx", ""), 1);
				return new ContentDownloadURLResponse(@"Assets/Content/Character/GaiaOnline/GaiaOnlineAvatar_Root.prefab", 1);
			}
			else
				return await base.RequestDownloadURL(contentId);
		}
	}
}
