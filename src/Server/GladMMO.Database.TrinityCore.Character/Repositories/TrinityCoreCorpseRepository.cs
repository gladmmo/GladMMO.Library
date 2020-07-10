using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCorpseRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Corpse>, ITrinityCorpseRepository
	{
		public TrinityCoreCorpseRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}