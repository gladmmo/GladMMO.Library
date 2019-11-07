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
		private ISpellCastDispatcher SpellCastDispatcher { get; }

		public EntityImmediateCastSpellMessageHandler([NotNull] ISpellCastDispatcher spellCastDispatcher)
		{
			SpellCastDispatcher = spellCastDispatcher ?? throw new ArgumentNullException(nameof(spellCastDispatcher));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ImmediateCastSpellMessage message)
		{
			SpellCastDispatcher.DispatchSpellCast(message.PendingSpellData, state);
		}
	}
}
