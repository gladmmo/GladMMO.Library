using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreMailLevelRewardRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, MailLevelReward>, ITrinityMailLevelRewardRepository
	{
		public TrinityCoreMailLevelRewardRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}