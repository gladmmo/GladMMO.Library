using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//This listener only does one thing, but it looks like it a lot.
	//When the player's world representation exists in the server scene
	//it just sends a player creation event to the actual player
	//so that the player will actually spawn itself, and know how to correctly.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerCreatedSendPlayerCreationPayloadToPlayerEventListener : BaseSingleEventListenerInitializable<IEntityCreationFinishedEventSubscribable, EntityCreationFinishedEventArgs>
	{
		private INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> Sender { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> EntityDataUpdateFactory { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public PlayerCreatedSendPlayerCreationPayloadToPlayerEventListener(IEntityCreationFinishedEventSubscribable subscriptionService, 
			INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> sender, 
			IReadonlyEntityGuidMappable<IMovementData> movementDataMappable, 
			IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> entityDataUpdateFactory, 
			IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable) 
			: base(subscriptionService)
		{
			Sender = sender;
			MovementDataMappable = movementDataMappable;
			EntityDataUpdateFactory = entityDataUpdateFactory;
			EntityDataMappable = entityDataMappable;
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			//TODO: Hide this in base class.
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			IMovementData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);
			IEntityDataFieldContainer dataFieldContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);

			EntityCreationData data = new EntityCreationData(args.EntityGuid, movementData, EntityDataUpdateFactory.Create(new EntityFieldUpdateCreationContext(dataFieldContainer, dataFieldContainer.DataSetIndicationArray)));

			var senderContext = new GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>(args.EntityGuid, new PlayerSelfSpawnEventPayload(data));

			Sender.Send(senderContext);
		}
	}
}
