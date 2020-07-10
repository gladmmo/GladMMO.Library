using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreRespawnRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt16, Respawn>, ITrinityRespawnRepository
	{
		public TrinityCoreRespawnRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}