using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class TrinityCoreCreatureTextRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, uint, NpcText>, ITrinityCreatureTextRepository
	{
		public TrinityCoreCreatureTextRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}
