using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreCharacterRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, int, Characters>, ITrinityCharacterRepository
	{
		public TrinityCoreCharacterRepository(wotlk_charactersContext context) 
			: base(context)
		{

		}

		public async Task<string> RetrieveNameAsync(int key)
		{
			Characters character = await Context
				.Characters
				.FindAsync(key);

			return character.Name;
		}

		public async Task<Characters> RetrieveAsync(string characterName)
		{
			Characters character = await Context
				.Characters
				.FirstAsync(c => c.Name == characterName.ToUpperInvariant());

			return character;
		}

		public async Task<bool> ContainsAsync(string characterName)
		{
			return await Context
				.Characters
				.AnyAsync(c => c.Name == characterName.ToUpperInvariant());
		}

		public async Task<int[]> CharacterIdsForAccountId(int accountId)
		{
			//TODO: uint
			return await Context
				.Characters
				.Where(c => c.Account == accountId)
				.Select(c => c.Guid)
				.Cast<int>()
				.ToArrayAsync();
		}
	}
}
