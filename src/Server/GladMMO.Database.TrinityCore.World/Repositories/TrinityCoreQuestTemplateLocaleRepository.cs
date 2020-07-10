using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreQuestTemplateLocaleRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, QuestTemplateLocale>, ITrinityQuestTemplateLocaleRepository
	{
		public TrinityCoreQuestTemplateLocaleRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}