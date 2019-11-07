using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellEffectTargetValidator
	{
		bool ValidateTargetContext(SpellDefinitionDataModel spellDefinition, SpellEffectDefinitionDataModel spellEffect, DefaultEntityActorStateContainer actorState);
	}
}
