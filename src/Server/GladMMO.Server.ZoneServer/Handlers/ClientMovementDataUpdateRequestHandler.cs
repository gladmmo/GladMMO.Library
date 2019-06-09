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
	public sealed class ClientMovementDataUpdateRequestHandler : ControlledEntityRequestHandler<ClientMovementDataUpdateRequest>
	{
		private IEntityGuidMappable<IMovementData> MovementDataMap { get; }

		private IReadonlyEntityGuidMappable<CharacterController> CharacterControllerMappable { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerator { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public ClientMovementDataUpdateRequestHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator,
			[NotNull] IReadonlyEntityGuidMappable<CharacterController> characterControllerMappable,
			[NotNull] IReadonlyNetworkTimeService timeService) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			MovementGenerator = movementGenerator ?? throw new ArgumentNullException(nameof(movementGenerator));
			CharacterControllerMappable = characterControllerMappable ?? throw new ArgumentNullException(nameof(characterControllerMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientMovementDataUpdateRequest payload, NetworkEntityGuid guid)
		{
			try
			{
				IMovementGenerator<GameObject> generator = MovementGenerator.RetrieveEntity(guid);
				IMovementData movementData = MovementDataMap.RetrieveEntity(guid);
				PositionChangeMovementData changeMovementData = BuildPositionChangeMovementData(payload, generator, movementData);
				MovementDataMap.ReplaceObject(guid, changeMovementData);

				//If the generator is running, we should use its initial position instead of the last movement data's position.
				MovementGenerator.ReplaceObject(guid, BuildCharacterControllerMovementGenerator(guid, changeMovementData, generator, movementData));
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to update MovementData for GUID: {guid} Reason: {e.Message}");

				throw;
			}

			return Task.CompletedTask;
		}

		private CharacterControllerInputMovementGenerator BuildCharacterControllerMovementGenerator(NetworkEntityGuid guid, PositionChangeMovementData data, IMovementGenerator<GameObject> generator, IMovementData movementData)
		{
			return new CharacterControllerInputMovementGenerator(data, new Lazy<CharacterController>(() => this.CharacterControllerMappable.RetrieveEntity(guid)));
		}

		private PositionChangeMovementData BuildPositionChangeMovementData(ClientMovementDataUpdateRequest payload, IMovementGenerator<GameObject> generator, IMovementData originalMovementData)
		{
			return new PositionChangeMovementData(TimeService.CurrentLocalTime, generator.isRunning ? generator.CurrentPosition : originalMovementData.InitialPosition, payload.MovementInput, originalMovementData.Rotation);
		}
	}
}
