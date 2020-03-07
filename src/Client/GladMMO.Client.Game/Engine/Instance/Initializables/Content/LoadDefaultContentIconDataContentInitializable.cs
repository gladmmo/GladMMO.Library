using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LoadDefaultContentIconDataContentInitializable : IGameInitializable
	{
		private IStaticContentDataServiceClient DataClient { get; }

		private IContentIconDataCollection ContentIconCollection { get; }

		public LoadDefaultContentIconDataContentInitializable([NotNull] IStaticContentDataServiceClient dataClient,
			[NotNull] IContentIconDataCollection contentIconCollection)
		{
			DataClient = dataClient ?? throw new ArgumentNullException(nameof(dataClient));
			ContentIconCollection = contentIconCollection ?? throw new ArgumentNullException(nameof(contentIconCollection));
		}

		public async Task OnGameInitialized()
		{
			ContentIconInstanceModel[] iconModels = await DataClient.ContentIconsAsync();

			foreach (var icon in iconModels)
			{
				ContentIconCollection.Add(icon.IconId, icon);
			}
		}
	}
}
