using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreLfgDungeonTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, LfgDungeonTemplate>, ITrinityLfgDungeonTemplateRepository
	{
		public TrinityCoreLfgDungeonTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}