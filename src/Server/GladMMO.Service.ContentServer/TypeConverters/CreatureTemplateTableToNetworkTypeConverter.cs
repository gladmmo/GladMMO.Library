using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class CreatureTemplateTableToNetworkTypeConverter : ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel>
	{
		public CreatureTemplateModel Convert(CreatureTemplateEntryModel fromObject)
		{
			return new CreatureTemplateModel(fromObject.CreatureTemplateId, fromObject.ModelId, fromObject.CreatureName, fromObject.MinimumLevel, fromObject.MaximumLevel);
		}
	}
}
