using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePvpstatsBattlegroundsRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt64, PvpstatsBattlegrounds>, ITrinityPvpstatsBattlegroundsRepository
	{
		public TrinityCorePvpstatsBattlegroundsRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}