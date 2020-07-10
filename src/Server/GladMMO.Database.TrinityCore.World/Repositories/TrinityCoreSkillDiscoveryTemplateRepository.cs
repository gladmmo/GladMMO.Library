using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSkillDiscoveryTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, SkillDiscoveryTemplate>, ITrinitySkillDiscoveryTemplateRepository
	{
		public TrinityCoreSkillDiscoveryTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}