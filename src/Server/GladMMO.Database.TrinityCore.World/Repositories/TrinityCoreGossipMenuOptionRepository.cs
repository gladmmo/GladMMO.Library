using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGossipMenuOptionRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, GossipMenuOption>, ITrinityGossipMenuOptionRepository
	{
		public TrinityCoreGossipMenuOptionRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}