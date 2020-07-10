using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSpellProcRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Int32, SpellProc>, ITrinitySpellProcRepository
	{
		public TrinityCoreSpellProcRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}