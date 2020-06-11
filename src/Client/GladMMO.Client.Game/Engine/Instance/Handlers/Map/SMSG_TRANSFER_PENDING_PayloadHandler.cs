using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_TRANSFER_PENDING_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_TRANSFER_PENDING_Payload>
	{
		private ILoadingScreenManagementService LoadingScreenService { get; }

		private IInstanceSceneChangeRequestedEventPublisher SceneChangePublisher { get; }

		private IClientDataCollectionContainer ClientData { get; }

		public SMSG_TRANSFER_PENDING_PayloadHandler(ILog logger,
			[NotNull] ILoadingScreenManagementService loadingScreenService,
			[NotNull] IInstanceSceneChangeRequestedEventPublisher sceneChangePublisher,
			[NotNull] IClientDataCollectionContainer clientData) 
			: base(logger)
		{
			LoadingScreenService = loadingScreenService ?? throw new ArgumentNullException(nameof(loadingScreenService));
			SceneChangePublisher = sceneChangePublisher ?? throw new ArgumentNullException(nameof(sceneChangePublisher));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
		}

		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_TRANSFER_PENDING_Payload payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Pending Map transfer to MapId: {payload.MapId}");

			LoadingScreenService.EnableLoadingScreenForMap(payload.MapId);

			//Now we're showing the loading screen of the map we will be headed to, we need to stop client message handling
			//so we can prepare to surgically extract to the new map instance.
			//But we cannot do this directly, we just have to do this indirectly by broadcasting a scene change event.
			//TODO: Remove old PSO playable game scene enum
			SceneChangePublisher.PublishEvent(this, new RequestedSceneChangeEventArgs(PlayableGameScene.Episode1Pioneer2, payload.MapId));

			//We cannot do this on the networking thread, it must be on next frame
			await UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				//Unload all scenes, we need to remove the current instance server scene
				//and the current map scene.
				await GladMMOSceneManager.UnloadAllAddressableScenesAsync();

				/*AsyncOperationHandle worldLoadHandle = GladMMOSceneManager.LoadAddressableSceneAsync(new MapFilePath(map.Directory));
				worldLoadHandle.Completed += op => GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME);*/

				//Assume the map exists
				MapEntry<string> map = ClientData.AssertEntry<MapEntry<string>>(payload.MapId);

				//Now we should load the two maps one on top of eachother.
				try
				{
					//TODO: Once ASYNC Addressabel loading doesn't RANDOMLY throw fucking NULL REFs fucking UNITY we don't need this nested disaster.
					GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(new MapFilePath(map.Directory), true)
						.Completed += handle =>
					{
						GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.INSTANCE_SERVER_SCENE_NAME)
							.Completed += handle2 =>
						{
							//Now that we've loaded successfully, we need to tell the server we've completed
							//with the teleport ACK
							//TODO: This even get sent after disabling??
							context.PayloadSendService.SendMessage(new MSG_MOVE_WORLDPORT_ACK_Payload());
						};
					};
				}
				catch (Exception e)
				{
					throw new InvalidOperationException($"Failed to load Map: {map.MapId} Directory: {map.Directory}. Reason: {e.Message}", e);
				}
			});
		}
	}
}
 