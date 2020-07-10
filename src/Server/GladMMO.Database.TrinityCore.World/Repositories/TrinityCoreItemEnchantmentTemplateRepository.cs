using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreItemEnchantmentTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, ItemEnchantmentTemplate>, ITrinityItemEnchantmentTemplateRepository
	{
		public TrinityCoreItemEnchantmentTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}