using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerNetworkTrackerChangeUpdateRequestHandler : ControlledEntityRequestHandler<PlayerNetworkTrackerChangeUpdateRequest>
	{
		private IReadonlyEntityGuidMappable<InterestCollection> InterestCollections { get; }

		private IEntitySessionMessageSender EntityMessageSender { get; }

		public PlayerNetworkTrackerChangeUpdateRequestHandler(ILog logger, IReadonlyConnectionEntityCollection connectionIdToEntityMap, IContextualResourceLockingPolicy<ObjectGuid> lockingPolicy,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> interestCollections, 
			[NotNull] IEntitySessionMessageSender entityMessageSender) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			InterestCollections = interestCollections ?? throw new ArgumentNullException(nameof(interestCollections));
			EntityMessageSender = entityMessageSender ?? throw new ArgumentNullException(nameof(entityMessageSender));
		}

		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, PlayerNetworkTrackerChangeUpdateRequest payload, ObjectGuid guid)
		{
			//TODO: Do some validation here. Players could be sending empty ones, or invalid ones.
			InterestCollection interestCollection = InterestCollections.RetrieveEntity(guid);
			PlayerNetworkTrackerChangeUpdateEvent changeUpdateEvent = new PlayerNetworkTrackerChangeUpdateEvent(new EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest>(guid, payload));

			//Only send to players and not yourself.
			foreach(ObjectGuid entity in interestCollection.ContainedEntities)
				if (entity.EntityType == EntityType.Player && entity.EntityId != guid.EntityId)
					EntityMessageSender.SendMessageAsync(entity, changeUpdateEvent);

			return Task.CompletedTask;
		}
	}
}
