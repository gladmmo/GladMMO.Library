using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultSpellDataCollection : ISpellDataCollection, IReadonlySpellDataCollection
	{
		private Dictionary<int, SpellDefinitionDataModel> SpellMap { get; } = new Dictionary<int, SpellDefinitionDataModel>(500);

		private Dictionary<int, SpellEffectDefinitionDataModel> SpellEffectMap { get; } = new Dictionary<int, SpellEffectDefinitionDataModel>(500);

		public IEnumerator<SpellDefinitionDataModel> GetEnumerator()
		{
			return SpellMap.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<SpellEffectDefinitionDataModel> GetEffectsForSpell(int spellId)
		{
			//TODO: Eventually we'll have more than 1 effect here.
			yield return SpellEffectMap[SpellMap[spellId].SpellEffectIdOne];
		}

		public SpellEffectDefinitionDataModel GetEffectForSpellAtIndex(int spellId, SpellEffectIndex effectIndex)
		{
			if((int)effectIndex != 0)
				throw new NotImplementedException($"TODO: Implement more index handling.");

			return SpellEffectMap[SpellMap[spellId].SpellEffectIdOne];
		}

		public bool ContainsSpellDefinition(int spellId)
		{
			return SpellMap.ContainsKey(spellId);
		}

		public bool AddSpellDefinition(SpellDefinitionDataModel spellData)
		{
			if (ContainsSpellDefinition(spellData.SpellId))
				return false;

			SpellMap.Add(spellData.SpellId, spellData);
			return true;
		}
	}
}
