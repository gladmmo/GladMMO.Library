using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSpellGroupStackRulesRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, SpellGroupStackRules>, ITrinitySpellGroupStackRulesRepository
	{
		public TrinityCoreSpellGroupStackRulesRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}