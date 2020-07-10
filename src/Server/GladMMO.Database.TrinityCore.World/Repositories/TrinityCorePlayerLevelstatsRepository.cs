using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayerLevelstatsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, PlayerLevelstats>, ITrinityPlayerLevelstatsRepository
	{
		public TrinityCorePlayerLevelstatsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}