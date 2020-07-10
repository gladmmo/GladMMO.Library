using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreItemTemplateLocaleRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, ItemTemplateLocale>, ITrinityItemTemplateLocaleRepository
	{
		public TrinityCoreItemTemplateLocaleRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}