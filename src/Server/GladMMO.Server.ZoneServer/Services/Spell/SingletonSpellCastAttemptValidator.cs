using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GladMMO
{
	public sealed class SingletonSpellCastAttemptValidator : ISpellCastAttemptValidator
	{
		private ISpellCastValidator[] Validators { get; }

		public SingletonSpellCastAttemptValidator([NotNull] IEnumerable<ISpellCastValidator> validators)
		{
			if (validators == null) throw new ArgumentNullException(nameof(validators));

			Validators = validators.ToArray();
		}

		public SpellCastResult ValidateSpellCast(DefaultEntityActorStateContainer actorState, int spellId)
		{
			foreach (var validator in Validators)
			{
				SpellCastResult result = validator.ValidateSpellCast(actorState, spellId);

				//If not successful we failed and no longer need to validate. We know something is wrong.
				if (result != SpellCastResult.SPELL_FAILED_SUCCESS)
					return result;
			}

			return SpellCastResult.SPELL_FAILED_SUCCESS;
		}
	}
}
