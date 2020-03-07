using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class ApplySpellEffectMessage : EntityActorMessage, ISpellEffectPairable
	{
		/// <summary>
		/// The GUID of the casting source.
		/// Could indicate self or could indicate an enemy player or creature.
		/// </summary>
		public ObjectGuid SourceCaster { get; }

		/// <summary>
		/// The actual spell that attached <see cref="SpellEffect"/> is attempting
		/// to apply to the entity actor.
		/// </summary>
		public SpellDefinitionDataModel Spell { get; }

		/// <summary>
		/// The effect to apply.
		/// </summary>
		public SpellEffectDefinitionDataModel SpellEffect { get; }

		public ApplySpellEffectMessage([NotNull] ObjectGuid sourceCaster, [NotNull] SpellDefinitionDataModel spell, [NotNull] SpellEffectDefinitionDataModel spellEffect)
		{
			SourceCaster = sourceCaster ?? throw new ArgumentNullException(nameof(sourceCaster));
			Spell = spell ?? throw new ArgumentNullException(nameof(spell));
			SpellEffect = spellEffect ?? throw new ArgumentNullException(nameof(spellEffect));

			//TODO: Expand this to validate additional effects.
			if(spell.SpellEffectIdOne != SpellEffect.SpellEffectId)
				throw new InvalidOperationException($"Cannot apply spell effect for Spell: {spell.SpellId} with Effect: {spellEffect.SpellEffectId} because the spell is not linked to that effect.");
		}
	}
}
