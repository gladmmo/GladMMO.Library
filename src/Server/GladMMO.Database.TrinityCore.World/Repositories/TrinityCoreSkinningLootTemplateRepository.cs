using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSkinningLootTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, SkinningLootTemplate>, ITrinitySkinningLootTemplateRepository
	{
		public TrinityCoreSkinningLootTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}