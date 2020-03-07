using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class LevelLearnedSpellsDatabaseToTransportTypeConverter : ITypeConverterProvider<SpellLevelLearned, SpellLevelLearnedDefinition>
	{
		public SpellLevelLearnedDefinition Convert([NotNull] SpellLevelLearned fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new SpellLevelLearnedDefinition(fromObject.CharacterClassType, fromObject.LevelLearned, fromObject.SpellId);
		}
	}
}
