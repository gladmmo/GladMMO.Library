using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventSaveRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, Byte, GameEventSave>, ITrinityGameEventSaveRepository
	{
		public TrinityCoreGameEventSaveRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}