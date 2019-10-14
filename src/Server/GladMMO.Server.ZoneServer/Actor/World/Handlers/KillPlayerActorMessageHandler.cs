using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class KillPlayerActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, KillPlayerActorMessage>
	{
		private IEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public KillPlayerActorMessageHandler([NotNull] IEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, KillPlayerActorMessage message)
		{
			//The player's actor has been killed.
			//Let's count ourselves as nobody now.
			ActorReferenceMappable.ReplaceObject(message.EntityGuid, Nobody.Instance);
		}
	}
}
