using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyLearnedSpellsCollection : IEnumerable<SpellLevelLearnedDefinition>
	{
		/// <summary>
		/// Indicates if a spell <see cref="spellId"/> is known/learned give the input composite key
		/// <see cref="classType"/> and <see cref="level"/>.
		/// </summary>
		/// <param name="spellId">The ID of the spell to check.</param>
		/// <param name="classType">The class type.</param>
		/// <param name="level">The level to check for.</param>
		/// <returns>True if the spell is learned/known.</returns>
		bool IsSpellKnown(int spellId, EntityPlayerClassType classType, int level);
	}

	public interface ILearnedSpellsCollection : IReadonlyLearnedSpellsCollection
	{
		/// <summary>
		/// Adds a new defined <see cref="levelLearnedDefinition"/>.
		/// </summary>
		/// <param name="levelLearnedDefinition">The level learned definition.</param>
		void Add(SpellLevelLearnedDefinition levelLearnedDefinition);
	}
}
