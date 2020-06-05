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

		private IEntityGuidMappable<MovementInfo> MovementInfoMappable { get; }

		//Dependency where we can forward for local movement prediction.
		private MSG_MOVE_PayloadHandler MovementPacketHandler { get; }

		private bool isCurrentlyStrafing { get; set; } = false;

		public OnMoveInputChangeNetworkSendInputEventListener(IMovementInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] ILocalPlayerDetails playerDetails, MSG_MOVE_PayloadHandler movementPacketHandler,
			[NotNull] IEntityGuidMappable<MovementInfo> movementInfoMappable) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			MovementPacketHandler = movementPacketHandler;
			MovementInfoMappable = movementInfoMappable ?? throw new ArgumentNullException(nameof(movementInfoMappable));
		}

		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			//We also are going to send a position hint to the server.
			//Server has authority in rejecting this hint, and it should if it finds its
			//WAY off. However this is how we deal with the issue of desyncronization
			//by having the client be semi-authorative about where it is.
			WorldTransform worldTransformComponent = TransformMap.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			GamePacketPayload payloadToSend = default;
			MovementInfo movementInfo = default;

			//Did we stop?
			if (!args.isMoving)
			{
				isCurrentlyStrafing = false;
				movementInfo = BuildStopMovementInfo(worldTransformComponent);
				payloadToSend = new MSG_MOVE_STOP_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo);
			}
			else
			{
				//It might SEEM like we should go right here.
				if (args.NewHorizontalInput < 0 && Math.Abs(args.NewVerticalInput) < float.Epsilon)
				{
					payloadToSend = BuildStrafeLeftPayload(args, worldTransformComponent, out movementInfo);
				}
				else if(args.NewHorizontalInput > 0 && Math.Abs(args.NewVerticalInput) < float.Epsilon)
				{
					payloadToSend = BuildStrafeRightPayload(args, worldTransformComponent, out movementInfo);
				}
				else if (args.NewVerticalInput > 0)
				{
					//moving forward
					payloadToSend = BuildForwardMovePayload(args, worldTransformComponent, out movementInfo);
				}
				else if (args.NewVerticalInput < 0)
				{
					//moving backwards
					payloadToSend = BuildBackwardsMovePayload(args, worldTransformComponent, out movementInfo);
				}

				//TODO: Handle other cases.
			}

			if (payloadToSend != default)
			{
				MovementInfoMappable.ReplaceObject(PlayerDetails.LocalPlayerGuid, movementInfo);
				SendService.SendMessage(payloadToSend);
				MovementPacketHandler.SpoofLocal(payloadToSend);
			}
		}

		private GamePacketPayload BuildForwardMovePayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent, out MovementInfo movementInfo)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildForwardMovementInfo(args, worldTransformComponent));

			return new MSG_MOVE_START_FORWARD_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildForwardMovementInfo(args, worldTransformComponent));
		}

		private GamePacketPayload BuildBackwardsMovePayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent, out MovementInfo movementInfo)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildBackwardsMovementInfo(args, worldTransformComponent));

			return new MSG_MOVE_START_BACKWARD_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildBackwardsMovementInfo(args, worldTransformComponent));
		}

		private GamePacketPayload BuildStrafeRightPayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent, out MovementInfo movementInfo)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildRightStrafeMovementInfo(args, worldTransformComponent));

			return new MSG_MOVE_START_STRAFE_RIGHT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildRightStrafeMovementInfo(args, worldTransformComponent));
		}

		private GamePacketPayload BuildStrafeLeftPayload(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent, out MovementInfo movementInfo)
		{
			if(args.isHeartBeat)
				return new MSG_MOVE_HEARTBEAT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildLeftStrafeMovementInfo(args, worldTransformComponent));

			return new MSG_MOVE_START_STRAFE_LEFT_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), movementInfo = BuildLeftStrafeMovementInfo(args, worldTransformComponent));
		}

		private MovementInfo BuildForwardMovementInfo(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(args.BuildMovementFlags(), MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private MovementInfo BuildBackwardsMovementInfo(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(args.BuildMovementFlags(), MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private MovementInfo BuildRightStrafeMovementInfo(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(args.BuildMovementFlags(), MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private MovementInfo BuildLeftStrafeMovementInfo(MovementInputChangedEventArgs args, WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(args.BuildMovementFlags(), MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}

		private static float CalculateWoWMovementInfoRotation(WorldTransform worldTransformComponent)
		{
			//See TrinityCore: Position::NormalizeOrientation
			return -(worldTransformComponent.YAxisRotation / 360.0f) * 2.0f * (float)Math.PI;
		}

		private MovementInfo BuildStopMovementInfo(WorldTransform worldTransformComponent)
		{
			Vector3 position = new Vector3(worldTransformComponent.PositionX, worldTransformComponent.PositionY, worldTransformComponent.PositionZ);

			return new MovementInfo(MovementFlag.MOVEMENTFLAG_NONE, MovementFlagExtra.None, (uint)TimeService.CurrentRemoteTime, position.ToWoWVector(), CalculateWoWMovementInfoRotation(worldTransformComponent), null, 0, 0, 0, null, 0);
		}
	}
}
