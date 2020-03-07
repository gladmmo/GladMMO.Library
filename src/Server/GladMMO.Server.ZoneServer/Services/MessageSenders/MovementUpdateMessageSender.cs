using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class MovementUpdateMessageSender : INetworkMessageSender<EntityMovementMessageContext>
	{
		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SessionMappable { get; }

		private IReadonlyEntityGuidMappable<InterestCollection> GuidToInterestCollectionMappable { get; }

		private IDirtyableMovementDataCollection MovementDataMap { get; }

		/// <inheritdoc />
		public MovementUpdateMessageSender(
			[NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sessionMappable, 
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> guidToInterestCollectionMappable, 
			[NotNull] IDirtyableMovementDataCollection movementDataMap)
		{
			SessionMappable = sessionMappable ?? throw new ArgumentNullException(nameof(sessionMappable));
			GuidToInterestCollectionMappable = guidToInterestCollectionMappable ?? throw new ArgumentNullException(nameof(guidToInterestCollectionMappable));
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
		}

		/// <inheritdoc />
		public void Send(EntityMovementMessageContext context)
		{
			//When they call this, they intend to send a movement update to the connection associated
			//with the ObjectGuid provided so we lookup the session associated with it
			//as well as the interest list and movement data for each individual entry int he internest
			//collection to build the movement packet.
			if(!SessionMappable.ContainsKey(context.EntityGuid))
				throw new InvalidOperationException($"Tried to send movement update to Session with Guid: {context.EntityGuid} but none existed.");

			//it's possible they aren't interested or have an empty interest so they may have no collection.
			if(!GuidToInterestCollectionMappable.ContainsKey(context.EntityGuid))
				return;

			EntityAssociatedData<IMovementData>[] movementBlocks = BuildMovementBlocks(context.EntityGuid);

			//it is possible that no movement data needs to be sent, because none is ddirty so we need to check
			if(movementBlocks.Length == 0)
				return;

			MovementDataUpdateEventPayload movementUpdateEvent = new MovementDataUpdateEventPayload(movementBlocks);

			SessionMappable[context.EntityGuid].SendMessage(movementUpdateEvent);
		}

		//TODO: We need to filter in ONLY dirty movement data. Right now it resends movement data every packet but we only want to update if the data has changed.
		private EntityAssociatedData<IMovementData>[] BuildMovementBlocks(ObjectGuid guid)
		{
			return GuidToInterestCollectionMappable[guid]
				.ContainedEntities
				//TODO: Temporarily we are not sending movement data about ourselves.
				//We also only send information about movement that is dirty from the last update we sent out.
				.Where(e => MovementDataMap.isEntryDirty(e)) 
				.Select(e => new EntityAssociatedData<IMovementData>(e, MovementDataMap[e]))
				.ToArray();
		}

		/// <inheritdoc />
		public Task SendAsync(EntityMovementMessageContext context)
		{
			throw new NotImplementedException("TODO: Implement the async version");
		}
	}
}
