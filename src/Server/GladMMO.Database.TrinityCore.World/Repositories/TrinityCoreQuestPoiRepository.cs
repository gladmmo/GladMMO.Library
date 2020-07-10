using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreQuestPoiRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, QuestPoi>, ITrinityQuestPoiRepository
	{
		public TrinityCoreQuestPoiRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}