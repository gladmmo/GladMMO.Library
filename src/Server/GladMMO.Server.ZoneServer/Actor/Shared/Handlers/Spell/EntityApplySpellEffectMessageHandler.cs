using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class EntityApplySpellEffectMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, ApplySpellEffectMessage>
	{
		private ISpellEffectHandlerFactory SpellEffectHandlerFactory { get; }

		public EntityApplySpellEffectMessageHandler([NotNull] ISpellEffectHandlerFactory spellEffectHandlerFactory)
		{
			SpellEffectHandlerFactory = spellEffectHandlerFactory ?? throw new ArgumentNullException(nameof(spellEffectHandlerFactory));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, ApplySpellEffectMessage message)
		{
			SpellEffectHandlerFactory.Create(message)
				.ApplySpellEffect(new SpellEffectApplicationContext(message.SourceCaster, state.EntityGuid, message, state.EntityData));
		}
	}
}
