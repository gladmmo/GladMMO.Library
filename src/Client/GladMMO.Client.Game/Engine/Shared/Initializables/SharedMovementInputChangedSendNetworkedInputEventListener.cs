using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	/*[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class SharedMovementInputChangedSendNetworkedInputEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>, IGameTickable
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		private ILog Logger { get; }

		//TODO: Put GameObject in player details
		private ILocalPlayerDetails PlayerDetails { get; }

		private IReadonlyEntityGuidMappable<GameObject> WorldRepresentationMappable { get; }

		//TODO: Flags/State
		private bool isHeartBeatSending { get; set; } = false;

		/// <inheritdoc />
		public SharedMovementInputChangedSendNetworkedInputEventListener(IMovementInputChangedEventSubscribable subscriptionService, 
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService, 
			[NotNull] ILog logger,
			[NotNull] ILocalPlayerDetails playerDetails,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> worldRepresentationMappable) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			WorldRepresentationMappable = worldRepresentationMappable ?? throw new ArgumentNullException(nameof(worldRepresentationMappable));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			Logger.Info($"About to send movement change.");

			if(isHeartBeatSending && !args.isMoving)
			{
				//Send stop
				Logger.Info($"About to send movement stop.");
				throw new NotImplementedException($"TODO: Reimplement movement.");
				//Vector3<float> freecraftVector = ComputeNetworkVector(WorldRepresentationMappable[PlayerDetails.LocalPlayerGuid]);
				//SendService.SendMessage(new MSG_MOVE_STOP_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), ComputeMovementInfo(freecraftVector, WorldRepresentationMappable[PlayerDetails.LocalPlayerGuid].transform.eulerAngles.y, MovementFlag.MOVEMENTFLAG_NONE)));
			}
			else if(!isHeartBeatSending && args.isMoving)
			{
				Logger.Info($"About to send movement start.");
				throw new NotImplementedException($"TODO: Reimplement movement.");
				//Vector3<float> freecraftVector = ComputeNetworkVector(WorldRepresentationMappable[PlayerDetails.LocalPlayerGuid]);
				//SendService.SendMessage(new MSG_MOVE_START_FORWARD_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), ComputeMovementInfo(freecraftVector, WorldRepresentationMappable[PlayerDetails.LocalPlayerGuid].transform.eulerAngles.y)));
			}

			isHeartBeatSending = args.isMoving;
		}

		/// <inheritdoc />
		public void Tick()
		{
			if(isHeartBeatSending)
			{
				GameObject go = WorldRepresentationMappable[PlayerDetails.LocalPlayerGuid];
				go.transform.position += go.transform.forward * Time.deltaTime * 5.0f;
				Vector3<float> freecraftVector = ComputeNetworkVector(go);

				throw new NotImplementedException($"TODO: Reimplement movement.");
				//SendService.SendMessage(new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), ComputeMovementInfo(freecraftVector, go.transform.eulerAngles.y)));
			}
		}

		private static Vector3<float> ComputeNetworkVector(GameObject go)
		{
			Vector3 unityPos = go.transform.position;
			Vector3<float> freecraftVector = unityPos.ToWoWVector();
			return freecraftVector;
		}

		private static MovementInfo ComputeMovementInfo(Vector3<float> freecraftVector, float orientation, MovementFlag moveFlags = MovementFlag.MOVEMENTFLAG_FORWARD)
		{
			return new MovementInfo(moveFlags, MovementFlagExtra.None, 0, freecraftVector, orientation, null, 0, 0, 0, null, 0);
		}
	}*/
}
