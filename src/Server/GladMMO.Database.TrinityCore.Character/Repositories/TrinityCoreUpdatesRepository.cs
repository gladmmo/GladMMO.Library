using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreUpdatesRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, String, Updates>, ITrinityUpdatesRepository
	{
		public TrinityCoreUpdatesRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}