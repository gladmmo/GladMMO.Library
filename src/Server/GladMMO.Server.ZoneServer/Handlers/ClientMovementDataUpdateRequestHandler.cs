using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
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

		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		/// <inheritdoc />
		public ClientMovementDataUpdateRequestHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator,
			[NotNull] IReadonlyEntityGuidMappable<CharacterController> characterControllerMappable,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			MovementGenerator = movementGenerator ?? throw new ArgumentNullException(nameof(movementGenerator));
			CharacterControllerMappable = characterControllerMappable ?? throw new ArgumentNullException(nameof(characterControllerMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
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

				IActorRef playerActorRef = ActorReferenceMappable.RetrieveEntity(guid);

				playerActorRef.TellSelf(new PlayerMovementStateChangedMessage(changeMovementData.Direction));

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
			//TODO: Sanity check timestamp and position.
			//We used to use the last generators current position
			//However we now use the hint position from the client.
			//This NEEDS to be sanity checked before used.
			//This semi-authorative approach is less secure but more responsive for the user.
			return new CharacterControllerInputMovementGenerator(data, new Lazy<CharacterController>(() => this.CharacterControllerMappable.RetrieveEntity(guid)), data.InitialPosition);
		}

		private PositionChangeMovementData BuildPositionChangeMovementData(ClientMovementDataUpdateRequest payload, IMovementGenerator<GameObject> generator, IMovementData originalMovementData)
		{
			//TODO: Sanity check timestamp and position.
			//So, originally we used authorative time and position but now we semi-trust the client.
			//We need to verify the send timestamp is not too far off and also sanity check the position too.
			return new PositionChangeMovementData(payload.Timestamp, payload.CurrentClientPosition, payload.MovementInput, originalMovementData.Rotation);
		}
	}
}
