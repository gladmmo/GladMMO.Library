using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//The reason we do this BEFORE entity creation beings
	//is that SO many entity creation event listeners
	//depend on the existence of the entity data container.
	//Therefore, before we event being to create the entity we must allocate
	//the entity data container and associate it.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeEntityDataContainerEventListener : BaseSingleEventListenerInitializable<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs>
	{
		//TODO: Unify these.
		private IEntityGuidMappable<IEntityDataFieldContainer> EntityDataContainer { get; }

		private IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		//Rare instance a user may want to access the raw and direct entity data collection
		//One such case is the serialization of the entire data collection for persistence.
		private IEntityGuidMappable<EntityFieldDataCollection> DataCollectionMappable { get; }

		public InitializeEntityDataContainerEventListener(IEntityCreationRequestedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection,
			[NotNull] IEntityGuidMappable<EntityFieldDataCollection> dataCollectionMappable) 
			: base(subscriptionService)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
			DataCollectionMappable = dataCollectionMappable ?? throw new ArgumentNullException(nameof(dataCollectionMappable));
		}

		protected override void OnEventFired(object source, EntityCreationRequestedEventArgs args)
		{
			NetworkEntityGuid guid = args.EntityGuid;

			//TODO: handle non-players
			//TODO: Fix the issue with having to hardcore the field count.
			//Build the update values stuff and initialize the initial movement data.
			EntityFieldDataCollection container = new EntityFieldDataCollection(ComputeEntityDataFieldLength(args.EntityGuid));

			DataCollectionMappable.AddObject(guid, container);
			ChangeTrackableCollection.AddObject(guid, new ChangeTrackingEntityFieldDataCollectionDecorator(container));
			EntityDataContainer.AddObject(guid, ChangeTrackableCollection.RetrieveEntity(guid));
		}

		private int ComputeEntityDataFieldLength([NotNull] NetworkEntityGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			switch (guid.EntityType)
			{
				case EntityType.Player:
					return GladMMOCommonConstants.PLAYER_DATA_FIELD_SIZE;
				case EntityType.GameObject:
					return GladMMOCommonConstants.GAMEOBJECT_DATA_FIELD_SIZE;
				case EntityType.Creature:
					return GladMMOCommonConstants.PLAYER_DATA_FIELD_SIZE; //TODO: Creature and player size should be different.
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
