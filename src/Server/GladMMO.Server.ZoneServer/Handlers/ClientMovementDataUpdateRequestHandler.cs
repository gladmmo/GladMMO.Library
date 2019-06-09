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

		/// <inheritdoc />
		public ClientMovementDataUpdateRequestHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator,
			[NotNull] IReadonlyEntityGuidMappable<CharacterController> characterControllerMappable) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			MovementGenerator = movementGenerator ?? throw new ArgumentNullException(nameof(movementGenerator));
			CharacterControllerMappable = characterControllerMappable ?? throw new ArgumentNullException(nameof(characterControllerMappable));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientMovementDataUpdateRequest payload, NetworkEntityGuid guid)
		{
			try
			{
				MovementGenerator.ReplaceObject(guid, new PlayerInputChangeMovementGenerator(payload.MovementInput, data => MovementDataMap.ReplaceObject(guid, data), CharacterControllerMappable.RetrieveEntity(guid)));
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
