using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IPlayerRotationChangeEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClientRotationDataUpdateRequestHandler : ControlledEntityRequestHandler<ClientRotationDataUpdateRequest>, IPlayerRotationChangeEventSubscribable
	{
		public event EventHandler<PlayerRotiationChangeEventArgs> OnPlayerRotationChanged;

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

				//TODO: This is a temporary hack, we nee d abetter solluition
				if (movementData is PositionChangeMovementData posChangeMoveDat)
				{
					Vector2 direction = posChangeMoveDat.Direction;

					//TODO: Sanity check position sent.
					//TODO: Sanity check timestamp
					MovementDataMap.ReplaceObject(guid, new PositionChangeMovementData(payload.TimeStamp, payload.ClientCurrentPosition, direction, payload.Rotation));
				}
				else
					throw new NotImplementedException($"TODO: Implement rotation when dealing with: {movementData.GetType().Name} type movement.");

				OnPlayerRotationChanged?.Invoke(this, new PlayerRotiationChangeEventArgs(guid, payload.Rotation));
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
