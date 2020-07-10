using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePvpstatsPlayersRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt64, PvpstatsPlayers>, ITrinityPvpstatsPlayersRepository
	{
		public TrinityCorePvpstatsPlayersRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}