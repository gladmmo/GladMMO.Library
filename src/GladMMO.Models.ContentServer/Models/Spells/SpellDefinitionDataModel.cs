﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class SpellDefinitionDataModel : ISpellDefinition
	{
		[JsonProperty]
		public int SpellId { get; private set; }

		[JsonProperty]
		public string SpellName { get; private set; }

		[JsonProperty]
		public SpellClassType SpellType { get; private set; }

		[JsonProperty]
		public int CastTime { get; private set; }

		[JsonProperty]
		public int SpellEffectIdOne { get; private set; }

		[JsonProperty]
		public int SpellIconId { get; private set; }

		//TODO: Enable these effects eventually.
		[JsonIgnore]
		private int SpellEffectIdTwo { get; set; }

		[JsonIgnore]
		private int SpellEffectIdThree { get; set; }

		public SpellDefinitionDataModel(int spellId, string spellName, SpellClassType spellType, int castTime, int spellEffectIdOne, int spellIconId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			if (spellEffectIdOne < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectIdOne));
			if (castTime < 0) throw new ArgumentOutOfRangeException(nameof(castTime));
			if (!Enum.IsDefined(typeof(SpellClassType), spellType)) throw new InvalidEnumArgumentException(nameof(spellType), (int) spellType, typeof(SpellClassType));
			if (spellIconId <= 0) throw new ArgumentOutOfRangeException(nameof(spellIconId));

			SpellId = spellId;
			SpellName = spellName;
			SpellType = spellType;
			CastTime = castTime;
			SpellEffectIdOne = spellEffectIdOne;
			SpellIconId = spellIconId;
		}

		public int GetSpellEffectId(SpellEffectIndex effectIndex)
		{
			switch (effectIndex)
			{
				case SpellEffectIndex.SpellEffectIndexOne:
					return SpellEffectIdOne;
				case SpellEffectIndex.SpellEffectIndexTwo:
				case SpellEffectIndex.SpellEffectIndexThree:
				default:
					throw new ArgumentOutOfRangeException(nameof(effectIndex), effectIndex, $"TODO: Implement additional spell effect index.");
			}
		}

		public IEnumerable<SpellEffectIndex> EnumerateSpellEffects()
		{
			if (SpellEffectIdOne != 0)
				yield return SpellEffectIndex.SpellEffectIndexOne;

			if (SpellEffectIdTwo != 0)
				yield return SpellEffectIndex.SpellEffectIndexTwo;

			if (SpellEffectIdThree != 0)
				yield return SpellEffectIndex.SpellEffectIndexThree;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private SpellDefinitionDataModel()
		{
			
		}
	}
}
