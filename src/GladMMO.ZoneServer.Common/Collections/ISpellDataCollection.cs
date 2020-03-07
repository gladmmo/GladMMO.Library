using System; using FreecraftCore;
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

		bool ContainsSpellEffectDefinition(int spellEffectId);

		SpellDefinitionDataModel GetSpellDefinition(int spellId);

		SpellEffectDefinitionDataModel GetSpellEffectDefinition(int spellEffectId);
	}

	public interface ISpellDataCollection : IReadonlySpellDataCollection
	{
		bool AddSpellDefinition(SpellDefinitionDataModel spellData);

		bool AddSpellEffectDefinition(SpellEffectDefinitionDataModel spellEffectData);
	}
}
