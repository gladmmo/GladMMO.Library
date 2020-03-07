using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class SpellEffectDefinitionDataModel : ISpellEffectDefinition
	{
		[JsonProperty]
		public int SpellEffectId { get; private set; }

		[JsonProperty]
		public SpellEffectType EffectType { get; private set; }

		[JsonProperty]
		public float BasePointsAdditiveLevelModifier { get; private set; }

		[JsonProperty]
		public int EffectBasePoints { get; private set; }

		[JsonProperty]
		public int EffectPointsDiceRange { get; private set; }

		[JsonProperty]
		public SpellEffectTargetType EffectTargetingType { get; private set; }

		[JsonProperty]
		public SpellEffectTargetType AdditionalEffectTargetingType { get; private set; }

		public SpellEffectDefinitionDataModel(int spellEffectId, SpellEffectType effectType, float basePointsAdditiveLevelModifier, int effectBasePoints, int effectPointsDiceRange, SpellEffectTargetType effectTargetingType, SpellEffectTargetType additionalEffectTargetingType)
		{
			if (spellEffectId < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectId));
			if (!Enum.IsDefined(typeof(SpellEffectType), effectType)) throw new InvalidEnumArgumentException(nameof(effectType), (int) effectType, typeof(SpellEffectType));
			if (!Enum.IsDefined(typeof(SpellEffectTargetType), effectTargetingType)) throw new InvalidEnumArgumentException(nameof(effectTargetingType), (int) effectTargetingType, typeof(SpellEffectTargetType));
			if (!Enum.IsDefined(typeof(SpellEffectTargetType), additionalEffectTargetingType)) throw new InvalidEnumArgumentException(nameof(additionalEffectTargetingType), (int) additionalEffectTargetingType, typeof(SpellEffectTargetType));

			SpellEffectId = spellEffectId;
			EffectType = effectType;
			BasePointsAdditiveLevelModifier = basePointsAdditiveLevelModifier;
			EffectBasePoints = effectBasePoints;
			EffectPointsDiceRange = effectPointsDiceRange;
			EffectTargetingType = effectTargetingType;
			AdditionalEffectTargetingType = additionalEffectTargetingType;
		}
	}
}
