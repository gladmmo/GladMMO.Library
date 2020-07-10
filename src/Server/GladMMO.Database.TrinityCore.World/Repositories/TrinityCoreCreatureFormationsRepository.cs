using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCreatureFormationsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, CreatureFormations>, ITrinityCreatureFormationsRepository
	{
		public TrinityCoreCreatureFormationsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}