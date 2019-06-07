using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	/// <summary>
	/// The concept of this initializable is that we wait until character session data
	/// has changed and then download the upcoming scene. Maybe it's a Lobby, maybe it's a Forest.
	/// Either way, we want to download it.
	/// </summary>
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class PreBurstQueryDownloadableSceneInitializable : ThreadUnSafeBaseSingleEventListenerInitializable<ICharacterSessionDataChangedEventSubscribable, CharacterSessionDataChangedEventArgs>
	{
		private IZoneServerService ZoneDataService { get; }

		private IContentServerServiceClient ContentService { get; }

		private ILog Logger { get; }

		public PreBurstQueryDownloadableSceneInitializable(ICharacterSessionDataChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IContentServerServiceClient contentService,
			[NotNull] IZoneServerService zoneDataService) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ContentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
			ZoneDataService = zoneDataService ?? throw new ArgumentNullException(nameof(zoneDataService));
		}

		protected override void OnThreadUnSafeEventFired(object source, CharacterSessionDataChangedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Starting process to download world.");

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

					//Can't do web request not on the main thread, sadly.
					await new UnityYieldAwaitable();

					//TODO: Do we need to be on the main unity3d thread
					UnityWebRequestAsyncOperation asyncOperation = UnityWebRequestAssetBundle.GetAssetBundle(urlDownloadResponse.DownloadURL, 0).SendWebRequest();

					//TODO: We should render these operations to the loading screen UI.
					asyncOperation.completed += operation =>
					{
						AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(asyncOperation.webRequest);

						string[] paths = bundle.GetAllScenePaths();

						foreach(string p in paths)
							Debug.Log($"Found Scene in Bundle: {p}");

						AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(System.IO.Path.GetFileNameWithoutExtension(paths.First()));

						sceneAsync.completed += operation1 =>
						{
							//When the scene is finished loading we should cleanup the asset bundle
							//Don't clean up the WHOLE BUNDLE, just the compressed downloaded data
							bundle.Unload(false);

							//TODO: We need a way/system to reference the bundle later so it can be cleaned up inbetween scene loads.
						};

						sceneAsync.allowSceneActivation = true;
					};
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to query for Download URL for ZoneId: {args.ZoneIdentifier} WorldId: {worldId} (0 if never succeeded request). Error: {e.Message}");

					Console.WriteLine(e);
					throw;
				}
			});
		}
	}
}
