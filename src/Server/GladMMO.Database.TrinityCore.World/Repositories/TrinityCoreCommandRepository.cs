using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCommandRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, String, Command>, ITrinityCommandRepository
	{
		public TrinityCoreCommandRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}