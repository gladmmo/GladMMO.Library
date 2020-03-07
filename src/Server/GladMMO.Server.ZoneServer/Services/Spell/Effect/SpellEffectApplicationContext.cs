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
		public ObjectGuid SpellSource { get; }

		/// <summary>
		/// The target guid of the spell.
		/// </summary>
		public ObjectGuid ApplicationTarget { get; }

		/// <summary>
		/// The spell data for the application.
		/// </summary>
		public ISpellEffectPairable SpellEffectData { get; }

		/// <summary>
		/// The readonly entity data for the application target/handler.
		/// </summary>
		public IReadonlyEntityDataFieldContainer ApplicationTargetEntityData { get; }

		public SpellEffectApplicationContext([NotNull] ObjectGuid spellSource, 
			[NotNull] ObjectGuid applicationTarget, 
			[NotNull] ISpellEffectPairable spellEffectData, 
			[NotNull] IReadonlyEntityDataFieldContainer applicationTargetEntityData)
		{
			SpellSource = spellSource ?? throw new ArgumentNullException(nameof(spellSource));
			ApplicationTarget = applicationTarget ?? throw new ArgumentNullException(nameof(applicationTarget));
			SpellEffectData = spellEffectData ?? throw new ArgumentNullException(nameof(spellEffectData));
			ApplicationTargetEntityData = applicationTargetEntityData ?? throw new ArgumentNullException(nameof(applicationTargetEntityData));
		}
	}
}
