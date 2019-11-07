using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SpellEffectTargetingStrategy(SpellEffectTargetType.TARGET_UNIT_TARGET_ENEMY, SpellEffectTargetType.NO_TARGET)]
	public sealed class SimpleEnemyTargetSpellEffectTargetValidator : BaseTargetSpellEffectValidator
	{
		protected override bool ValidateTargeting(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState)
		{
			if(spellDefinition == null) throw new ArgumentNullException(nameof(spellDefinition));
			if(spellEffect == null) throw new ArgumentNullException(nameof(spellEffect));
			if(actorState == null) throw new ArgumentNullException(nameof(actorState));

			NetworkEntityGuid guid = GetEntityTarget(actorState);

			//TODO: We shouldn't assume they are enemies just because they aren't use. We need a system for hostility masking for entities
			return guid != actorState.EntityGuid;
		}
	}
}
