using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventGameobjectRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, SByte, GameEventGameobject>, ITrinityGameEventGameobjectRepository
	{
		public TrinityCoreGameEventGameobjectRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}