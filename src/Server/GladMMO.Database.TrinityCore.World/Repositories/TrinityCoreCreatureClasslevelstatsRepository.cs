using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCreatureClasslevelstatsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, CreatureClasslevelstats>, ITrinityCreatureClasslevelstatsRepository
	{
		public TrinityCoreCreatureClasslevelstatsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}