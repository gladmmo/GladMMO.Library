using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	/// <summary>
	/// The concept of this initializable is that we wait until character session data
	/// has changed and then download the upcoming scene. Maybe it's a Lobby, maybe it's a Forest.
	/// Either way, we want to download it.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IWorldDownloadBeginEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class PreBurstQueryDownloadableSceneInitializable : ThreadUnSafeBaseSingleEventListenerInitializable<ICharacterSessionDataChangedEventSubscribable, CharacterSessionDataChangedEventArgs>, IWorldDownloadBeginEventSubscribable
	{
		private ILog Logger { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private ILoadingScreenManagementService LoadingScreenService { get; }

		public event EventHandler<WorldDownloadBeginEventArgs> OnWorldDownloadBegins;

		public PreBurstQueryDownloadableSceneInitializable(ICharacterSessionDataChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] ILoadingScreenManagementService loadingScreenService) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
		}

		protected override void OnThreadUnSafeEventFired(object source, CharacterSessionDataChangedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Loading MapId: {args.ZoneIdentifier}");

			MapEntry<string> map = ClientData.AssertEntry<MapEntry<string>>(args.ZoneIdentifier);

			AsyncOperationHandle worldLoadHandle = GladMMOSceneManager.LoadAddressableSceneAsync(new MapFilePath(map.Directory));
			LoadingScreenService.RegisterOperation(worldLoadHandle);

			worldLoadHandle.Completed += op => GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME);
			OnWorldDownloadBegins?.Invoke(this, new WorldDownloadBeginEventArgs(worldLoadHandle));
		}
	}
}
