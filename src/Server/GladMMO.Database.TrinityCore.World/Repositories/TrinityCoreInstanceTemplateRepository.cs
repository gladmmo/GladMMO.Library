using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreInstanceTemplateRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt16, InstanceTemplate>, ITrinityInstanceTemplateRepository
	{
		public TrinityCoreInstanceTemplateRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}