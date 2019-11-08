using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SpellEffectApplicationContext
	{
		/// <summary>
		/// The source guid of the spell.
		/// (Ex. the caster).
		/// </summary>
		public NetworkEntityGuid SpellSource { get; }

		/// <summary>
		/// The target guid of the spell.
		/// </summary>
		public NetworkEntityGuid ApplicationTarget { get; }

		/// <summary>
		/// The spell data for the application.
		/// </summary>
		public ISpellEffectPairable SpellEffectData { get; }

		public SpellEffectApplicationContext([NotNull] NetworkEntityGuid spellSource, [NotNull] NetworkEntityGuid applicationTarget, [NotNull] ISpellEffectPairable spellEffectData)
		{
			SpellSource = spellSource ?? throw new ArgumentNullException(nameof(spellSource));
			ApplicationTarget = applicationTarget ?? throw new ArgumentNullException(nameof(applicationTarget));
			SpellEffectData = spellEffectData ?? throw new ArgumentNullException(nameof(spellEffectData));
		}
	}
}
