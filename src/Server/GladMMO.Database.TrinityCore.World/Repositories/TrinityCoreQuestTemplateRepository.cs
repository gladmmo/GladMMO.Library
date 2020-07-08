using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class TrinityCoreQuestTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, uint, QuestTemplate>, ITrinityQuestTemplateRepository
	{
		public TrinityCoreQuestTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}
