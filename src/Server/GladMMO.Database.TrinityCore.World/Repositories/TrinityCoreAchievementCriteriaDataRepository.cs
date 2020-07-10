using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreAchievementCriteriaDataRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Int32, AchievementCriteriaData>, ITrinityAchievementCriteriaDataRepository
	{
		public TrinityCoreAchievementCriteriaDataRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}