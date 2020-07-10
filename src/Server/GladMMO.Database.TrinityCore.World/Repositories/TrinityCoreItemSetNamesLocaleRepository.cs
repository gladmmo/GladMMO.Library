using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreItemSetNamesLocaleRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, ItemSetNamesLocale>, ITrinityItemSetNamesLocaleRepository
	{
		public TrinityCoreItemSetNamesLocaleRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}