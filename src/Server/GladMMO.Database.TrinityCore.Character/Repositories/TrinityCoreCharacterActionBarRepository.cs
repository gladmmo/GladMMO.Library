using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreCharacterActionBarRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, CharacterAction>, ITrinityCharacterActionBarRepository
	{
		public TrinityCoreCharacterActionBarRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}

		public async Task<CharacterAction[]> RetrieveAllForCharacterAsync(int characterId)
		{
			//TODO: Support specs
			//TODO: Support more than 12 action buttons.
			return await Context.CharacterAction
				.Where(c => c.Guid == (uint) characterId && c.Spec == 0 && c.Button < (int)ActionBarIndex.ActionBarIndex_12) 
				.ToArrayAsync();
		}
	}
}
