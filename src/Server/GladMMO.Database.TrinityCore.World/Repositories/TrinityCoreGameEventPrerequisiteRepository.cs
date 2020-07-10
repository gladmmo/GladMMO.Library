using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventPrerequisiteRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEventPrerequisite>, ITrinityGameEventPrerequisiteRepository
	{
		public TrinityCoreGameEventPrerequisiteRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}