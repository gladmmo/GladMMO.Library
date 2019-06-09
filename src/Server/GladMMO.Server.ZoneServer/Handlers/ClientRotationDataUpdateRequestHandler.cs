using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClientRotationDataUpdateRequestHandler : ControlledEntityRequestHandler<ClientRotationDataUpdateRequest>
	{
		private IEntityGuidMappable<IMovementData> MovementDataMap { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerator { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public ClientRotationDataUpdateRequestHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator,
			[NotNull] IReadonlyNetworkTimeService timeService)
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			MovementGenerator = movementGenerator ?? throw new ArgumentNullException(nameof(movementGenerator));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientRotationDataUpdateRequest payload, NetworkEntityGuid guid)
		{
			try
			{
				IMovementGenerator<GameObject> generator = MovementGenerator.RetrieveEntity(guid);
				IMovementData movementData = MovementDataMap.RetrieveEntity(guid);
				Vector3 initialPosition = generator.isRunning ? generator.CurrentPosition : movementData.InitialPosition;

				//TODO: This is a temporary hack, we nee d abetter solluition
				Vector2 direction = movementData is PositionChangeMovementData d ? d.Direction : Vector2.zero;

				MovementDataMap.ReplaceObject(guid, new PositionChangeMovementData(TimeService.CurrentLocalTime, initialPosition, direction, payload.Rotation));
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to update MovementData for GUID: {guid} Reason: {e.Message}");

				throw;
			}

			return Task.CompletedTask;
		}
	}
}
