using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreReputationRewardRateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, ReputationRewardRate>, ITrinityReputationRewardRateRepository
	{
		public TrinityCoreReputationRewardRateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}