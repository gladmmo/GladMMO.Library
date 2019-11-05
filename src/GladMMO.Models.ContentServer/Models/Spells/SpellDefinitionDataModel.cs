using System;
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

		public SpellDefinitionDataModel(int spellId, string spellName, SpellClassType spellType, int castTime, int spellEffectIdOne)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			if (spellEffectIdOne < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectIdOne));
			if (castTime < 0) throw new ArgumentOutOfRangeException(nameof(castTime));
			if (!Enum.IsDefined(typeof(SpellClassType), spellType)) throw new InvalidEnumArgumentException(nameof(spellType), (int) spellType, typeof(SpellClassType));

			SpellId = spellId;
			SpellName = spellName;
			SpellType = spellType;
			CastTime = castTime;
			SpellEffectIdOne = spellEffectIdOne;
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
