using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class CreatureTemplateTableToNetworkTypeConverter : ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel>
	{
		public CreatureTemplateModel Convert([NotNull] CreatureTemplateEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new CreatureTemplateModel(fromObject.CreatureTemplateId, fromObject.ModelId, fromObject.CreatureName, fromObject.MinimumLevel, fromObject.MaximumLevel);
		}
	}
}
