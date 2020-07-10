using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreQuestPoolMembersRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, QuestPoolMembers>, ITrinityQuestPoolMembersRepository
	{
		public TrinityCoreQuestPoolMembersRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}