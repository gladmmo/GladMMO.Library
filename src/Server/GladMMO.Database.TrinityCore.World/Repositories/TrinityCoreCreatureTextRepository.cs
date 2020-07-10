using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCreatureTextRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, CreatureText>, ITrinityCreatureTextRepository
	{
		public TrinityCoreCreatureTextRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}