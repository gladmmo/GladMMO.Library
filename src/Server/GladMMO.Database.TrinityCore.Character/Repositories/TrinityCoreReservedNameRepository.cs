using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreReservedNameRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, String, ReservedName>, ITrinityReservedNameRepository
	{
		public TrinityCoreReservedNameRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}