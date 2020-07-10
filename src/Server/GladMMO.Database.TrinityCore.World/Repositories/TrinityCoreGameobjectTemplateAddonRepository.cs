using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameobjectTemplateAddonRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, GameobjectTemplateAddon>, ITrinityGameobjectTemplateAddonRepository
	{
		public TrinityCoreGameobjectTemplateAddonRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}