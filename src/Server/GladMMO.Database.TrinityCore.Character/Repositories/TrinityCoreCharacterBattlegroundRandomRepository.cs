using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCharacterBattlegroundRandomRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, CharacterBattlegroundRandom>, ITrinityCharacterBattlegroundRandomRepository
	{
		public TrinityCoreCharacterBattlegroundRandomRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}