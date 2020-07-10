using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreWardenChecksRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, WardenChecks>, ITrinityWardenChecksRepository
	{
		public TrinityCoreWardenChecksRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}