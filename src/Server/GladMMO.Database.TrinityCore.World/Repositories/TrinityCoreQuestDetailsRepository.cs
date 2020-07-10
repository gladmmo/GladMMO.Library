using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreQuestDetailsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, QuestDetails>, ITrinityQuestDetailsRepository
	{
		public TrinityCoreQuestDetailsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}