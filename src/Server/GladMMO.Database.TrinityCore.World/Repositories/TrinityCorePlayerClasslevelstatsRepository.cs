using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayerClasslevelstatsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, PlayerClasslevelstats>, ITrinityPlayerClasslevelstatsRepository
	{
		public TrinityCorePlayerClasslevelstatsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}