using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreWorldstatesRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Worldstates>, ITrinityWorldstatesRepository
	{
		public TrinityCoreWorldstatesRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}