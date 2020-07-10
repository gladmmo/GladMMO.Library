using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreInstanceSpawnGroupsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, InstanceSpawnGroups>, ITrinityInstanceSpawnGroupsRepository
	{
		public TrinityCoreInstanceSpawnGroupsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}