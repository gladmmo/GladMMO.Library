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

		public InitializeEntityDataContainerEventListener(IEntityCreationRequestedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection) 
			: base(subscriptionService)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
		}

		protected override void OnEventFired(object source, EntityCreationRequestedEventArgs args)
		{
			NetworkEntityGuid guid = args.EntityGuid;

			if(guid.EntityType != EntityType.Player)
				throw new NotImplementedException($"TODO: Implement handling for non-players.");

			//TODO: handle non-players
			//TODO: Fix the issue with having to hardcore the field count.
			//Build the update values stuff and initialize the initial movement data.
			ChangeTrackableCollection.AddObject(guid, new ChangeTrackingEntityFieldDataCollectionDecorator(new EntityFieldDataCollection<EUnitFields>(1328)));
			EntityDataContainer.AddObject(guid, ChangeTrackableCollection.RetrieveEntity(guid));
		}
	}
}
