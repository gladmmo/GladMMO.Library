using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	[SpellCastValidator]
	public sealed class MovementCastingCheckCastValidator : ISpellCastValidator
	{
		private IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		public MovementCastingCheckCastValidator([NotNull] IReadonlyEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable)
		{
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
		}

		public SpellCastResult ValidateSpellCast(DefaultEntityActorStateContainer state, int spellId)
		{
			if (!MovementGeneratorMappable.RetrieveEntity(state.EntityGuid).isFinished)
				return SpellCastResult.SPELL_FAILED_MOVING;

			return SpellCastResult.SPELL_FAILED_SUCCESS;
		}
	}
}
