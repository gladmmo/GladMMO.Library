using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventNpcflagRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEventNpcflag>, ITrinityGameEventNpcflagRepository
	{
		public TrinityCoreGameEventNpcflagRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}