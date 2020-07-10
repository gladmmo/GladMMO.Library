using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreConditionsRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Int32, Conditions>, ITrinityConditionsRepository
	{
		public TrinityCoreConditionsRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}