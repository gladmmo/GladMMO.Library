using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlySpellEffectLookupable
	{
		/// <summary>
		/// Produces an iterable collection of spell data effects
		/// for the spell with id <see cref="spellId"/>.
		/// </summary>
		/// <param name="spellId"></param>
		/// <returns></returns>
		IEnumerable<SpellEffectDefinitionDataModel> GetEffectsForSpell(int spellId);

		SpellEffectDefinitionDataModel GetEffectForSpellAtIndex(int spellId, SpellEffectIndex effectIndex);
	}

	public interface IReadonlySpellDataCollection : IEnumerable<SpellDefinitionDataModel>, IReadonlySpellEffectLookupable
	{
		bool ContainsSpellDefinition(int spellId);
	}

	public interface ISpellDataCollection : IReadonlySpellDataCollection
	{
		bool AddSpellDefinition(SpellDefinitionDataModel spellData);
	}
}
