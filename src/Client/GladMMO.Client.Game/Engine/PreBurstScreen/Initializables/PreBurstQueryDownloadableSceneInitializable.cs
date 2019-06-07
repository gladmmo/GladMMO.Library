using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	/// <summary>
	/// The concept of this initializable is that we wait until character session data
	/// has changed and then download the upcoming scene. Maybe it's a Lobby, maybe it's a Forest.
	/// Either way, we want to download it.
	/// </summary>
	public sealed class PreBurstQueryDownloadableSceneInitializable : ThreadUnSafeBaseSingleEventListenerInitializable<ICharacterSessionDataChangedEventSubscribable, CharacterSessionDataChangedEventArgs>
	{
		private IZoneServerService ZoneDataService { get; }

		private IContentServerServiceClient ContentService { get; }

		private ILog Logger { get; }

		public PreBurstQueryDownloadableSceneInitializable(ICharacterSessionDataChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnThreadUnSafeEventFired(object source, CharacterSessionDataChangedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				long worldId = 0;
				try
				{
					//TODO: Handle failure
					ProjectVersionStage.AssertAlpha();
					//TODO: Handle throwing/error
					//We need to know the world the zone is it, so we can request a download URL for it.
					worldId = await ZoneDataService.GetZoneWorld(args.ZoneIdentifier)
						.ConfigureAwait(false);

					//With the worldid we can get the download URL.
					ContentDownloadURLResponse urlDownloadResponse = await ContentService.RequestWorldDownloadUrl(worldId)
						.ConfigureAwait(false);

					if(Logger.IsInfoEnabled)
						Logger.Info($"World Download Url: {urlDownloadResponse.DownloadURL}");
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to query for Download URL for ZoneId: {args.ZoneIdentifier} WorldId: {worldId} (0 if never succeeded request).");

					Console.WriteLine(e);
					throw;
				}
			});
		}
	}
}
