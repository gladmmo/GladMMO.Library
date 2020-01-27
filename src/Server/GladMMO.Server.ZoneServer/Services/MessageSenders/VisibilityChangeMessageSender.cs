using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class VisibilityChangeMessageSender : INetworkMessageSender<EntityVisibilityChangeContext>
	{
		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SessionMappable { get; }

		private IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> EntityDataMapper { get; }

		private IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> FieldUpdateFactory { get; }

		/// <inheritdoc />
		public VisibilityChangeMessageSender(
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable, 
			[NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sessionMappable,
			[NotNull] IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> entityDataMapper,
			[NotNull] IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> fieldUpdateFactory)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			SessionMappable = sessionMappable ?? throw new ArgumentNullException(nameof(sessionMappable));
			EntityDataMapper = entityDataMapper ?? throw new ArgumentNullException(nameof(entityDataMapper));
			FieldUpdateFactory = fieldUpdateFactory ?? throw new ArgumentNullException(nameof(fieldUpdateFactory));
		}

		/// <inheritdoc />
		public void Send([NotNull] EntityVisibilityChangeContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			NetworkObjectVisibilityChangeEventPayload changeEventPayload = BuildPayload(context.InterestCollection);
			IPeerPayloadSendService<GameServerPacketPayload> sendService = RetrieveSendService(context.EntityGuid);

			sendService.SendMessage(changeEventPayload, DeliveryMethod.ReliableOrdered)
				.ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task SendAsync([NotNull] EntityVisibilityChangeContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			NetworkObjectVisibilityChangeEventPayload changeEventPayload = BuildPayload(context.InterestCollection);
			IPeerPayloadSendService<GameServerPacketPayload> sendService = RetrieveSendService(context.EntityGuid);

			//TODO: Should we await or return?
			await sendService.SendMessage(changeEventPayload, DeliveryMethod.ReliableOrdered)
				.ConfigureAwaitFalse();
		}

		private IPeerPayloadSendService<GameServerPacketPayload> RetrieveSendService([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return SessionMappable.RetrieveEntity(guid);
		}

		private NetworkObjectVisibilityChangeEventPayload BuildPayload([NotNull] IReadonlyInterestCollection interestCollection)
		{
			if(interestCollection == null) throw new ArgumentNullException(nameof(interestCollection));

			//TODO: Provide movement mappable
			InterestChangedPacketBuilder changedPacketBuilder = new InterestChangedPacketBuilder(MovementDataMappable, EntityDataMapper, FieldUpdateFactory);

			//We delegate the packet building to the packet builder. But we still need to send it.
			//Sending is async so it can be fired off and not awaited, we won't want to await it
			NetworkObjectVisibilityChangeEventPayload changeEventPayload = changedPacketBuilder.Build(interestCollection.QueuedJoiningEntities, interestCollection.QueuedLeavingEntities);
			return changeEventPayload;
		}
	}
}
