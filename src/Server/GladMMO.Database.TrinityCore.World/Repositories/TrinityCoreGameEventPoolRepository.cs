using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventPoolRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, SByte, GameEventPool>, ITrinityGameEventPoolRepository
	{
		public TrinityCoreGameEventPoolRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}