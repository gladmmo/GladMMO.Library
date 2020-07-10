using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCharacterActionRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, CharacterAction>, ITrinityCharacterActionRepository
	{
		public async Task<CharacterAction[]> RetrieveAllForCharacterAsync(int characterId)
		{
			//TODO: Support specs
			//TODO: Support more than 12 action buttons.
			return await Context.CharacterAction
				.Where(c => c.Guid == (uint) characterId && c.Spec == 0 && c.Button < (int)ActionBarIndex.ActionBarIndex_12) 
				.ToArrayAsync();
		}

		public async Task<bool> AddAllAsync(CharacterAction[] actions)
		{
			await Context.CharacterAction
				.AddRangeAsync(actions);

			return await Context.SaveChangesAsync() != 0;
		}
	}
}
