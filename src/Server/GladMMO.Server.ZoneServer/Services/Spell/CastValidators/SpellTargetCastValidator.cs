using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SpellCastValidator]
	public sealed class SpellTargetCastValidator : ISpellCastValidator
	{
		private ISpellTargetValidator TargetValidator { get; }

		public SpellTargetCastValidator([NotNull] ISpellTargetValidator targetValidator)
		{
			TargetValidator = targetValidator ?? throw new ArgumentNullException(nameof(targetValidator));
		}

		public SpellCastResult ValidateSpellCast(DefaultEntityActorStateContainer actorState, int spellId)
		{
			if (!TargetValidator.isSpellTargetViable(spellId, actorState))
				return SpellCastResult.SPELL_FAILED_BAD_TARGETS;

			return SpellCastResult.SPELL_FAILED_SUCCESS;
		}
	}
}
