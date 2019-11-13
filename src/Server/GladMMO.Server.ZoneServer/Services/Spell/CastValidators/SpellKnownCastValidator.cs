using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SpellCastValidator]
	public sealed class SpellKnownCastValidator : ISpellCastValidator
	{
		private IReadonlyLearnedSpellsCollection LearnedSpells { get; }

		public SpellKnownCastValidator([NotNull] IReadonlyLearnedSpellsCollection learnedSpells)
		{
			LearnedSpells = learnedSpells ?? throw new ArgumentNullException(nameof(learnedSpells));
		}

		public SpellCastResult ValidateSpellCast([NotNull] DefaultEntityActorStateContainer actorState, int spellId)
		{
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));

			int level = actorState.EntityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL);

			//TODO: Support correct entity class type.
			//If it's a player and we don't know the spell.
			if (actorState.EntityGuid.EntityType == EntityType.Player && !LearnedSpells.IsSpellKnown(spellId, EntityPlayerClassType.Mage, level))
				return SpellCastResult.SPELL_FAILED_NOT_KNOWN;

			return SpellCastResult.SPELL_FAILED_SUCCESS;
		}
	}
}
