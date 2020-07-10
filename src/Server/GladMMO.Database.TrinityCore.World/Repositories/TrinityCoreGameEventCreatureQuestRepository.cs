using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventCreatureQuestRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEventCreatureQuest>, ITrinityGameEventCreatureQuestRepository
	{
		public TrinityCoreGameEventCreatureQuestRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}