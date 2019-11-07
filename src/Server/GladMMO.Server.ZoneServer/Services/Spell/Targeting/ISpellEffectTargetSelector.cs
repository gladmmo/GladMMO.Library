using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellEffectTargetSelector
	{
		SpellEffectTargetContext CalculateTargets(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState, IPendingSpellCastData pendingSpellCast);
	}
}
