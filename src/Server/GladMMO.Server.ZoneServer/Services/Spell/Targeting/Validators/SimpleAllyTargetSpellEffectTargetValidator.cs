using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SpellEffectTargetingStrategy(SpellEffectTargetType.TARGET_UNIT_TARGET_ALLY, SpellEffectTargetType.NO_TARGET)]
	public sealed class SimpleAllyTargetSpellEffectTargetValidator : BaseTargetSpellEffectValidator
	{
		protected override bool ValidateTargeting(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState)
		{
			//Ally targeting is always valid. Even if targeting an enemy
			//This is because we'll just cast it on ourselves if we're
			//targeting an enemy.
			return true;
		}
	}
}
