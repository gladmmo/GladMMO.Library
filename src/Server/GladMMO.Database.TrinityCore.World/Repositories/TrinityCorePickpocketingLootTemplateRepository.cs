using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePickpocketingLootTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PickpocketingLootTemplate>, ITrinityPickpocketingLootTemplateRepository
	{
		public TrinityCorePickpocketingLootTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}