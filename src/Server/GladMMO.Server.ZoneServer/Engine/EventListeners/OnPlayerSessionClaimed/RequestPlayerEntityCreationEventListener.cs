using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class RequestPlayerEntityCreationEventListener : BaseSingleEventListenerInitializable<IPlayerSessionClaimedEventSubscribable, PlayerSessionClaimedEventArgs>
	{
		private IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> EntityCreationRequestPublisher { get; }

		public RequestPlayerEntityCreationEventListener(IPlayerSessionClaimedEventSubscribable subscriptionService,
			[NotNull] IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> entityCreationRequestPublisher) 
			: base(subscriptionService)
		{
			EntityCreationRequestPublisher = entityCreationRequestPublisher ?? throw new ArgumentNullException(nameof(entityCreationRequestPublisher));
		}

		protected override void OnEventFired(object source, PlayerSessionClaimedEventArgs args)
		{
			//When we encounter a claimed player session
			//we should publish a request to create an entity with the requested guid.
			EntityCreationRequestPublisher.PublishEvent(source, new EntityCreationRequestedEventArgs(args.EntityGuid));
		}
	}
}
