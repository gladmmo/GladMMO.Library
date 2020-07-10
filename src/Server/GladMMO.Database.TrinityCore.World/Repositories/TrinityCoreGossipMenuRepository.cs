using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGossipMenuRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, GossipMenu>, ITrinityGossipMenuRepository
	{
		public TrinityCoreGossipMenuRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}