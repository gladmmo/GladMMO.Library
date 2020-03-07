using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SpellEffectApplicationMessageCreationContext
	{
		/// <summary>
		/// The source of the spell effect application.
		/// Basically the casted of the linked spell.
		/// </summary>
		public ObjectGuid ApplicationSource { get; }

		/// <summary>
		/// Spell id to cast.
		/// </summary>
		public int SpellId { get; }

		/// <summary>
		/// The effect index to create an application message for.
		/// </summary>
		public SpellEffectIndex EffectIndex { get; }

		public SpellEffectApplicationMessageCreationContext([NotNull] ObjectGuid applicationSource, int spellId, SpellEffectIndex effectIndex)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			ApplicationSource = applicationSource ?? throw new ArgumentNullException(nameof(applicationSource));
			SpellId = spellId;
			EffectIndex = effectIndex;
		}
	}

	public interface ISpellEffectApplicationMessageFactory : IFactoryCreatable<ApplySpellEffectMessage, SpellEffectApplicationMessageCreationContext>
	{

	}
}
