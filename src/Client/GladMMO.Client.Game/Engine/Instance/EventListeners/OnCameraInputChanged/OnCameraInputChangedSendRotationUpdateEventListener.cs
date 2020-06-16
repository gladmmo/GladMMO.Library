using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnCameraInputChangedSendRotationUpdateEventListener : BaseSingleEventListenerInitializable<ICameraInputChangedEventSubscribable, CameraInputChangedEventArgs>
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IEntityGuidMappable<MovementInfo> MovementInfoMappable { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> WorldTransformMappable { get; }

		public OnCameraInputChangedSendRotationUpdateEventListener(ICameraInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IEntityGuidMappable<MovementInfo> transformMappable,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> worldTransformMappable) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			MovementInfoMappable = transformMappable ?? throw new ArgumentNullException(nameof(transformMappable));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			WorldTransformMappable = worldTransformMappable ?? throw new ArgumentNullException(nameof(worldTransformMappable));
		}

		protected override void OnEventFired(object source, CameraInputChangedEventArgs args)
		{
			MovementInfo info = MovementInfoMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			WorldTransform transform = WorldTransformMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			Vector3 currentPosition = new Vector3(transform.PositionX, transform.PositionY, transform.PositionZ);

			MovementInfo info2 = new MovementInfo(info.MoveFlags, info.ExtraFlags, CalculateMovementInfoTime(),
				currentPosition.ToWoWVector(), CalculateWoWMovementInfoRotation(args.Rotation), info.TransportationInformation, info.TransportationTime, 
				info.MovePitch, info.FallTime, info.FallData, info.SplineElevation);


			MovementInfoMappable.ReplaceObject(PlayerDetails.LocalPlayerGuid, info2);

			SendService.SendMessage(new MSG_MOVE_SET_FACING_Payload(new PackedGuid(PlayerDetails.LocalPlayerGuid), info2));
		}

		private uint CalculateMovementInfoTime()
		{
			return (uint)TimeService.MillisecondsSinceStartup;
		}

		private static float CalculateWoWMovementInfoRotation(float rotation)
		{
			//See TrinityCore: Position::NormalizeOrientation
			return -(rotation / 360.0f) * 2.0f * (float)Math.PI;
		}
	}
}
