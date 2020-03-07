using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	/*[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnMoveInputChangePredictMovementDataEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>
	{
		private MovementUpdateMessageHandler MovementUpdateHandler { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		private INetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		public OnMoveInputChangePredictMovementDataEventListener(IMovementInputChangedEventSubscribable subscriptionService,
			[NotNull] ILocalPlayerDetails playerDetails,
			[NotNull] INetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] MovementUpdateMessageHandler movementUpdateHandler,
			[NotNull] IEntityGuidMappable<MovementBlockData> movementDataMappable)
			: base(subscriptionService)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			MovementUpdateHandler = movementUpdateHandler ?? throw new ArgumentNullException(nameof(movementUpdateHandler));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			//Don't predict on heartbeat
			if (args.isHeartBeat)
				return;

			Vector2 direction = new Vector2(args.NewHorizontalInput, args.NewVerticalInput);
			WorldTransform worldTransform = TransformMap.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			long predictedTime = TimeService.CurrentRemoteTime;
			MovementBlockData data = new PositionChangeMovementData(predictedTime, new Vector3(worldTransform.PositionX, worldTransform.PositionY, worldTransform.PositionZ), direction, worldTransform.YAxisRotation);

			MovementDataMappable.ReplaceObject(PlayerDetails.LocalPlayerGuid, data);
			MovementUpdateHandler.HandleMovementUpdate(new EntityAssociatedData<MovementBlockData>(PlayerDetails.LocalPlayerGuid, data), true);
		}
	}*/
}
