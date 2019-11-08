using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultSpellEffectApplicationMessageFactory : ISpellEffectApplicationMessageFactory
	{
		private IReadonlySpellDataCollection SpellDataCollection { get; }

		public DefaultSpellEffectApplicationMessageFactory([NotNull] IReadonlySpellDataCollection spellDataCollection)
		{
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
		}

		public ApplySpellEffectMessage Create([NotNull] SpellEffectApplicationMessageCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			SpellDefinitionDataModel spellDefinition = SpellDataCollection.GetSpellDefinition(context.SpellId);
			SpellEffectDefinitionDataModel effectDefinition = SpellDataCollection.GetSpellEffectDefinition(spellDefinition.GetSpellEffectId(context.EffectIndex));

			return new ApplySpellEffectMessage(context.ApplicationSource, spellDefinition, effectDefinition);
		}
	}
}
