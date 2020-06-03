using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnMoveInputChangeNetworkSendInputEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> TransformMap { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		//Dependency where we can forward for local movement prediction.
		private MSG_MOVE_PayloadHandler MovementPacketHandler { get; }

		public OnMoveInputChangeNetworkSendInputEventListener(IMovementInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] ILocalPlayerDetails playerDetails, MSG_MOVE_PayloadHandler movementPacketHandler) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			MovementPacketHandler = movementPacketHandler;
		}

		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			//We send remote time instead of remoteTime + latency because
			//our client is going to move right away and we want EVERYONE
			//to view us as if we had started moving at the same time as the
			//local client percieves it.
			long timeStamp = TimeService.CurrentRemoteTime;

			//We also are going to send a position hint to the server.
			//Server has authority in rejecting this hint, and it should if it finds its
			//WAY off. However this is how we deal with the issue of desyncronization
			//by having the client be semi-authorative about where it is.
			WorldTransform worldTransformComponent = TransformMap.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			GamePacketPayload payloadToSend = default;

			//Did we stop?
			if (!args.isMoving)
			{
				payloadToSend = new MSG_MOVE_STOP_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), BuildStopMovementInfo(worldTransformComponent));
			}
			else
			{
				//We going left baby
				if (args.NewHorizontalInput < 0 && Math.Abs(args.NewVerticalInput) < float.Epsilon)
				{
					payloadToSend = BuildStrafeLeftPayload(args, worldTransformComponent);
				}
				else if(args.NewHorizontalInput > 0 && Math.Abs(args.NewVerticalInput) < float.Epsilon)
				{
					payloadToSend = BuildStrafeRightPayload(args, worldTransformComponent);
				}

				//TODO: Handle other cases.
			}

			if (payloadToSend != default)
			{
				SendService.SendMessage(payloadToSend);
				MovementPacketHandler.SpoofLocal(payloadToSend);
			}
		}

		private GamePacketPayload BuildStrafeRightPayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), BuildRightStrafeMovementInfo(worldTransformComponent));

			return new MSG_MOVE_START_STRAFE_RIGHT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), BuildRightStrafeMovementInfo(worldTransformComponent));
		}

		private GamePacketPayload BuildStrafeLeftPayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), BuildLeftStrafeMovementInfo(worldTransformComponent));

			return new MSG_MOVE_START_STRAFE_LEFT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), BuildLeftStrafeMovementInfo(worldTransformComponent));
		}

		private MovementInfo BuildRightStrafeMovementInfo(WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(MovementFlag.MOVEMENTFLAG_STRAFE_RIGHT, MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private MovementInfo BuildLeftStrafeMovementInfo(WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(MovementFlag.MOVEMENTFLAG_STRAFE_LEFT, MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private static float CalculateWoWMovementInfoRotation(WorldTransform worldTransformComponent)
		{
			//See TrinityCore: Position::NormalizeOrientation
			return (worldTransformComponent.YAxisRotation / 360.0f) * 2.0f * (float)Math.PI;
		}

		private MovementInfo BuildStopMovementInfo(WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(MovementFlag.MOVEMENTFLAG_NONE, MovementFlagExtra.None, (uint) TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}
	}
}
