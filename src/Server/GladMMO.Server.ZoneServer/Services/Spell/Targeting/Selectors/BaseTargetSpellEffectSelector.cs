using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseTargetSpellEffectSelector : ISpellEffectTargetSelector
	{
		public abstract SpellEffectTargetContext CalculateTargets(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState)

		protected NetworkEntityGuid GetEntityTarget([NotNull] DefaultEntityActorStateContainer state)
		{
			if(state == null) throw new ArgumentNullException(nameof(state));

			return state.EntityData.GetEntityGuidValue(EntityObjectField.UNIT_FIELD_TARGET); ;
		}
	}
}
