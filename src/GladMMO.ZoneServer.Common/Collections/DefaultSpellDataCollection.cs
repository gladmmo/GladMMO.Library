using System; using FreecraftCore;
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
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			//TODO: Eventually we'll have more than 1 effect here.
			yield return SpellEffectMap[SpellMap[spellId].SpellEffectIdOne];
		}

		public SpellEffectDefinitionDataModel GetEffectForSpellAtIndex(int spellId, SpellEffectIndex effectIndex)
		{
			if(spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			if((int)effectIndex != 0)
				throw new NotImplementedException($"TODO: Implement more index handling.");

			return SpellEffectMap[SpellMap[spellId].SpellEffectIdOne];
		}

		public bool ContainsSpellDefinition(int spellId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			return SpellMap.ContainsKey(spellId);
		}

		public bool ContainsSpellEffectDefinition(int spellEffectId)
		{
			if (spellEffectId < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectId));

			return SpellEffectMap.ContainsKey(spellEffectId);
		}

		public SpellDefinitionDataModel GetSpellDefinition(int spellId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			return SpellMap[spellId];
		}

		public SpellEffectDefinitionDataModel GetSpellEffectDefinition(int spellEffectId)
		{
			if (spellEffectId < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectId));

			return SpellEffectMap[spellEffectId];
		}

		public bool AddSpellDefinition([NotNull] SpellDefinitionDataModel spellData)
		{
			if (spellData == null) throw new ArgumentNullException(nameof(spellData));

			if (ContainsSpellDefinition(spellData.SpellId))
				return false;

			SpellMap.Add(spellData.SpellId, spellData);
			return true;
		}

		public bool AddSpellEffectDefinition([NotNull] SpellEffectDefinitionDataModel spellEffectData)
		{
			if (spellEffectData == null) throw new ArgumentNullException(nameof(spellEffectData));

			if(ContainsSpellEffectDefinition(spellEffectData.SpellEffectId))
				return false;

			SpellEffectMap.Add(spellEffectData.SpellEffectId, spellEffectData);
			return true;
		}
	}
}
