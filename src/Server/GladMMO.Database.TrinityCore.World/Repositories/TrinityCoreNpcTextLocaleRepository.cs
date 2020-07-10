using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreNpcTextLocaleRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, NpcTextLocale>, ITrinityNpcTextLocaleRepository
	{
		public TrinityCoreNpcTextLocaleRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}