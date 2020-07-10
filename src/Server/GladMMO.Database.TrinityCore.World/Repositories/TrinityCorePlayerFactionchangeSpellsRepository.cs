using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayerFactionchangeSpellsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PlayerFactionchangeSpells>, ITrinityPlayerFactionchangeSpellsRepository
	{
		public TrinityCorePlayerFactionchangeSpellsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}