using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePoolMembersRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, PoolMembers>, ITrinityPoolMembersRepository
	{
		public TrinityCorePoolMembersRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}