using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultLearnedSpellsCollection : ILearnedSpellsCollection, IReadonlyLearnedSpellsCollection
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

		public void Add([NotNull] SpellLevelLearnedDefinition levelLearnedDefinition)
		{
			if (levelLearnedDefinition == null) throw new ArgumentNullException(nameof(levelLearnedDefinition));

			long key = ComputeKey(levelLearnedDefinition.SpellId, levelLearnedDefinition.CharacterClassType);
			LevelLearnedDefinitions.Add(key, levelLearnedDefinition);
		}

		private long ComputeKey(int spellId, EntityPlayerClassType classType)
		{
			return ((long) spellId << 32) + (long)classType;
		}

		public IEnumerator<SpellLevelLearnedDefinition> GetEnumerator()
		{
			return LevelLearnedDefinitions.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
