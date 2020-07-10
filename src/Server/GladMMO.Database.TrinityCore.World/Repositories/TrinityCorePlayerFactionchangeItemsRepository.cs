using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayerFactionchangeItemsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PlayerFactionchangeItems>, ITrinityPlayerFactionchangeItemsRepository
	{
		public TrinityCorePlayerFactionchangeItemsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}