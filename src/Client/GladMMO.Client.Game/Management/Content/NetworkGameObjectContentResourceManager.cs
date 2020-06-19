using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public sealed class NetworkGameObjectContentResourceManager : DefaultLoadableContentResourceManager
	{
		private IClientDataCollectionContainer ClientData { get; }

		public NetworkGameObjectContentResourceManager(ILog logger,
			[NotNull] IClientDataCollectionContainer clientData)
			: base(logger, UserContentType.GameObject)
		{
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
		}

		protected override Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId)
		{
			if (!ClientData.HasEntry<GameObjectDisplayInfoEntry<string>>((int)contentId))
				return Task.FromResult(new ContentDownloadURLResponse(ContentDownloadURLResponseCode.NoContentId));

			GameObjectDisplayInfoEntry<string> entry = ClientData.AssertEntry<GameObjectDisplayInfoEntry<string>>((int) contentId);

			return Task.FromResult(new ContentDownloadURLResponse(entry.ModelPath.Replace(".mdx", ""), 1));
		}
	}
}
