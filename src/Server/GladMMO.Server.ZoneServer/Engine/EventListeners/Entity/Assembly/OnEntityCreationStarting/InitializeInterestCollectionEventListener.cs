using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeInterestCollectionEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<InterestCollection> GuidToInterestCollectionMappable { get; }

		public InitializeInterestCollectionEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<InterestCollection> guidToInterestCollectionMappable) 
			: base(subscriptionService)
		{
			GuidToInterestCollectionMappable = guidToInterestCollectionMappable ?? throw new ArgumentNullException(nameof(guidToInterestCollectionMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			InterestCollection playerInterestCollection = new InterestCollection();

			//directly add ourselves so we don't become interest in ourselves after spawning
			playerInterestCollection.Add(args.EntityGuid);

			//We just create our own manaul interest collection here.
			GuidToInterestCollectionMappable.AddObject(args.EntityGuid, playerInterestCollection);
		}
	}
}
