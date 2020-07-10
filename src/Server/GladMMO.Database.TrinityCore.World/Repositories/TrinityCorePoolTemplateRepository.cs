using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePoolTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PoolTemplate>, ITrinityPoolTemplateRepository
	{
		public TrinityCorePoolTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}