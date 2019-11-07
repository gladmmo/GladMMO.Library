using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseTargetSpellEffectValidator : ISpellEffectTargetValidator
	{
		public bool ValidateTargetContext([NotNull] SpellDefinitionDataModel spellDefinition, [NotNull] SpellEffectDefinitionDataModel spellEffect, [NotNull] DefaultEntityActorStateContainer actorState)
		{
			if (spellDefinition == null) throw new ArgumentNullException(nameof(spellDefinition));
			if (spellEffect == null) throw new ArgumentNullException(nameof(spellEffect));
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));

			//We don't validate effect targeting matching the validators effect targeting
			//because a validator may have MULTIPLE targeting handling.

			return ValidateTargeting(spellDefinition, spellEffect, actorState);
		}

		/// <summary>
		/// Parameters are never null.
		/// </summary>
		/// <param name="spellDefinition"></param>
		/// <param name="spellEffect"></param>
		/// <param name="actorState"></param>
		/// <returns></returns>
		protected abstract bool ValidateTargeting(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState);

		protected NetworkEntityGuid GetEntityTarget([NotNull] DefaultEntityActorStateContainer state)
		{
			if (state == null) throw new ArgumentNullException(nameof(state));

			return state.EntityData.GetEntityGuidValue(EntityObjectField.UNIT_FIELD_TARGET);;
		}
	}
}
