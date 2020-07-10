using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreExplorationBasexpRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, ExplorationBasexp>, ITrinityExplorationBasexpRepository
	{
		public TrinityCoreExplorationBasexpRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}