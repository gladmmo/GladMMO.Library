using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventConditionRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEventCondition>, ITrinityGameEventConditionRepository
	{
		public TrinityCoreGameEventConditionRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}