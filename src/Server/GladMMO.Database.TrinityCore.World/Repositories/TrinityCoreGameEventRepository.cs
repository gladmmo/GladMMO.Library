using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEvent>, ITrinityGameEventRepository
	{
		public TrinityCoreGameEventRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}