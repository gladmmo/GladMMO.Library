using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSpellLearnSpellRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, SpellLearnSpell>, ITrinitySpellLearnSpellRepository
	{
		public TrinityCoreSpellLearnSpellRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}