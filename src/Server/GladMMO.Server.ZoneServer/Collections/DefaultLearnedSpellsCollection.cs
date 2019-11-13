using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultLearnedSpellsCollection : ILearnedSpellsCollection
	{
		private Dictionary<long, SpellLevelLearnedDefinition> LevelLearnedDefinitions { get; } = new Dictionary<long, SpellLevelLearnedDefinition>(100);

		public bool IsSpellKnown(int spellId, EntityPlayerClassType classType, int level)
		{
			long key = ComputeKey(spellId, classType);

			if (LevelLearnedDefinitions.ContainsKey(key))
				return LevelLearnedDefinitions[key].LevelLearned <= level;
			else
				return false; //not a level learned spell.
		}

		private long ComputeKey(int spellId, EntityPlayerClassType classType)
		{
			return ((long) spellId << 32) + (long)classType;
		}
	}
}
