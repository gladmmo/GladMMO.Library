using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Initialization component that downloads the world scene.
	/// </summary>
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class DownloadWorldContentInitializable : IGameInitializable
	{
		private IDownloadableContentServerServiceClient ContentService { get; }

		private WorldConfiguration WorldConfig { get; }

		private ILog Logger { get; }

		public DownloadWorldContentInitializable([NotNull] IDownloadableContentServerServiceClient contentService,
			[NotNull] WorldConfiguration worldConfig,
			[NotNull] ILog logger)
		{
			ContentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
			WorldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task OnGameInitialized()
		{
			//TODO: Handle throwing/error
			ContentDownloadURLResponse downloadUrlResponse = await ContentService.RequestWorldDownloadUrl(WorldConfig.WorldId)
				.ConfigureAwait(false);

			if(Logger.IsInfoEnabled)
				Logger.Info($"World Download Url: {downloadUrlResponse.DownloadURL}");

			//Can't do web request not on the main thread, sadly.
			await new UnityYieldAwaitable();

			//TODO: Handle failure.
			WorldDownloader downloader = new WorldDownloader(Logger);

			//At this point after this finishes the world shoud be downloaded.
			await downloader.DownloadAsync(downloadUrlResponse.DownloadURL, null, true); //important that the world loads ontop of the existing scene.
		}
	}
}
