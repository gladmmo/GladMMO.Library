using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class WorldActorEntityActorCreationEventListener : EntityCreationFinishedEventListener
	{
		private IWorldActorRef WorldActor { get; }

		public WorldActorEntityActorCreationEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IWorldActorRef worldActor) 
			: base(subscriptionService)
		{
			WorldActor = worldActor ?? throw new ArgumentNullException(nameof(worldActor));
		}

		public WorldActorEntityActorCreationEventListener(IEntityCreationFinishedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			WorldActor.Tell(new CreateEntityActorMessage(args.EntityGuid));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			
		}
	}
}
