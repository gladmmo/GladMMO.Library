using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreQuestPoolTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, QuestPoolTemplate>, ITrinityQuestPoolTemplateRepository
	{
		public TrinityCoreQuestPoolTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}