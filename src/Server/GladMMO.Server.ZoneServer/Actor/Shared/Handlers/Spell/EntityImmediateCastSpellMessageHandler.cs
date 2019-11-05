using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	//This is all demo code.
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityImmediateCastSpellMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, ImmediateCastSpellMessage>
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public EntityImmediateCastSpellMessageHandler([NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ImmediateCastSpellMessage message)
		{
			//TODO: This is just demo code.
			//TODO: Handle targeting at the spell level.
			NetworkEntityGuid targetGuid = state.EntityData.GetEntityGuidValue(EntityObjectField.UNIT_FIELD_TARGET);

			if (targetGuid.isEmpty)
				return;

			if (!ActorReferenceMappable.ContainsKey(targetGuid))
				return;

			IActorRef targetActor = ActorReferenceMappable.RetrieveEntity(targetGuid);

			targetActor.Tell(new DamageEntityActorCurrentHealthMessage(1));
		}
	}
}
