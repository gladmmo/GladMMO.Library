using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class SpellEffectEntryDatabaseToTransportTypeConverter : ITypeConverterProvider<SpellEffectEntryModel, SpellEffectDefinitionDataModel>
	{
		public SpellEffectDefinitionDataModel Convert([NotNull] SpellEffectEntryModel effect)
		{
			if (effect == null) throw new ArgumentNullException(nameof(effect));

			return new SpellEffectDefinitionDataModel(effect.SpellEffectId, effect.EffectType, effect.BasePointsAdditiveLevelModifier, effect.EffectBasePoints, effect.EffectPointsDiceRange, effect.EffectTargetingType, effect.AdditionalEffectTargetingType);
		}
	}
}
