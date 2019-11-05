using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class SpellEntryDatabaseToTransportTypeConverter : ITypeConverterProvider<SpellEntryModel, SpellDefinitionDataModel>
	{
		public SpellDefinitionDataModel Convert(SpellEntryModel fromObject)
		{
			return new SpellDefinitionDataModel(fromObject.SpellId, fromObject.SpellName, fromObject.SpellType, fromObject.CastTime, fromObject.SpellEffectIdOne);
		}
	}
}
