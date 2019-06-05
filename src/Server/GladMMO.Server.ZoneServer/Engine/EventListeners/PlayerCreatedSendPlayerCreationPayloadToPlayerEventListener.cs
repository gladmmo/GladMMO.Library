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
	public sealed class PlayerCreatedSendPlayerCreationPayloadToPlayerEventListener : BaseSingleEventListenerInitializable<IPlayerWorldSessionCreatedEventSubscribable, PlayerWorldSessionCreationEventArgs>
	{
		private INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> Sender { get; }

		private IReadonlyEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> EntityDataUpdateFactory { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		/// <inheritdoc />
		public PlayerCreatedSendPlayerCreationPayloadToPlayerEventListener(
			IPlayerWorldSessionCreatedEventSubscribable subscriptionService, 
			[NotNull] INetworkMessageSender<GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>> sender, 
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementDataMappable,
			[NotNull] IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> entityDataUpdateFactory,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable) 
			: base(subscriptionService)
		{
			Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			EntityDataUpdateFactory = entityDataUpdateFactory ?? throw new ArgumentNullException(nameof(entityDataUpdateFactory));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, PlayerWorldSessionCreationEventArgs args)
		{
			IMovementData movementData = RetrieveEntity(MovementDataMappable, args.EntityGuid);
			IEntityDataFieldContainer dataFieldContainer = RetrieveEntity(EntityDataMappable, args.EntityGuid);

			EntityCreationData data = new EntityCreationData(args.EntityGuid, movementData, EntityDataUpdateFactory.Create(new EntityFieldUpdateCreationContext(dataFieldContainer, dataFieldContainer.DataSetIndicationArray)));

			var senderContext = new GenericSingleTargetMessageContext<PlayerSelfSpawnEventPayload>(args.EntityGuid, new PlayerSelfSpawnEventPayload(data));

			Sender.Send(senderContext);
		}

		public TReturnType RetrieveEntity<TReturnType>([NotNull] IReadonlyEntityGuidMappable<TReturnType> collection, [NotNull] NetworkEntityGuid guid)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			try
			{
				return collection[guid];
			}
			catch(Exception e)
			{
				throw new InvalidOperationException($"Failed to access {typeof(TReturnType).Name} from Entity: {guid}. Error: {e.Message}");
			}
		}
	}
}
