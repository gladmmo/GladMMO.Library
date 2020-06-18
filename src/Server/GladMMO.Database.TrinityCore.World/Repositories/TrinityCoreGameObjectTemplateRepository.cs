using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreGameObjectTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, uint, GameobjectTemplate>, ITrinityGameObjectTemplateRepository
	{
		public TrinityCoreGameObjectTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}
