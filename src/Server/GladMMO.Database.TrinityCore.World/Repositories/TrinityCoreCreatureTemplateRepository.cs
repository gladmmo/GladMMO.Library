using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreCreatureTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, uint, CreatureTemplate>, ITrinityCreatureTemplateRepository
	{
		public TrinityCoreCreatureTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}
