using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreUpdatesIncludeRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, String, UpdatesInclude>, ITrinityUpdatesIncludeRepository
	{
		public TrinityCoreUpdatesIncludeRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}