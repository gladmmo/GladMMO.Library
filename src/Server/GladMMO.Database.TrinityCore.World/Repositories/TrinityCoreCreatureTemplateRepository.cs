using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCreatureTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, CreatureTemplate>, ITrinityCreatureTemplateRepository
	{
		public TrinityCoreCreatureTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}