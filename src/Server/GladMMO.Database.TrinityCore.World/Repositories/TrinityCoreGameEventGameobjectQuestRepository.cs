using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGameEventGameobjectQuestRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, GameEventGameobjectQuest>, ITrinityGameEventGameobjectQuestRepository
	{
		public TrinityCoreGameEventGameobjectQuestRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}