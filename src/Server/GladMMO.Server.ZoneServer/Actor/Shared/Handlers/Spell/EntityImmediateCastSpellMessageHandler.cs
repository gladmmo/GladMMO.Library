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
		private ISpellTargetValidator TargetValidator { get; }

		private ISpellCastDispatcher SpellCastDispatcher { get; }

		public EntityImmediateCastSpellMessageHandler([NotNull] ISpellTargetValidator targetValidator,
			[NotNull] ISpellCastDispatcher spellCastDispatcher)
		{
			TargetValidator = targetValidator ?? throw new ArgumentNullException(nameof(targetValidator));
			SpellCastDispatcher = spellCastDispatcher ?? throw new ArgumentNullException(nameof(spellCastDispatcher));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ImmediateCastSpellMessage message)
		{
			//State could have changed since then.
			if (!TargetValidator.isSpellTargetViable(message.PendingSpellData.SpellId, state))
			{
				//TODO: Send failed packet
				return;
			}

			SpellCastDispatcher.DispatchSpellCast(message.PendingSpellData, state);
		}
	}
}
