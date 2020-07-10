using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventConditionSaveRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, Byte, GameEventConditionSave>, ITrinityGameEventConditionSaveRepository
	{
		public TrinityCoreGameEventConditionSaveRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}