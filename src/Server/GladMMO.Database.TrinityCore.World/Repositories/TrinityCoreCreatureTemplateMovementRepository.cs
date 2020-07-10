using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCreatureTemplateMovementRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, CreatureTemplateMovement>, ITrinityCreatureTemplateMovementRepository
	{
		public TrinityCoreCreatureTemplateMovementRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}