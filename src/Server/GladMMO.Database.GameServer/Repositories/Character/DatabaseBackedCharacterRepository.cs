using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterRepository : ICharacterRepository, IGenericRepositoryCrudable<string, CharacterEntryModel>
	{
		/// <summary>
		/// The database context.
		/// </summary>
		private CharacterDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedCharacterRepository(CharacterDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			Context = context;
		}

		public Task<bool> ContainsAsync(int key)
		{
			return Context
				.Characters
				.AnyAsync(c => c.CharacterId == key);
		}

		public Task<CharacterEntryModel> RetrieveAsync(string characterName)
		{
			return Context
				.Characters
				.FirstOrDefaultAsync(c => c.CharacterName == characterName);
		}

		public Task<bool> ContainsAsync(string key)
		{
			return Context
				.Characters
				.AnyAsync(c => c.CharacterName == key);
		}

		/// <inheritdoc />
		public async Task<string> RetrieveNameAsync(int key)
		{
			if(!await ContainsAsync(key))
				throw new InvalidOperationException($"Requested Character with Key: {key} but none exist.");

			CharacterEntryModel character = await Context
				.Characters
				.FindAsync(key);

			if(character == null)
				throw new InvalidOperationException($"Tried to load Character with Key: {key} from database. Expected to exist but null.");

			return character.CharacterName;
		}

		/// <inheritdoc />
		public async Task<int[]> CharacterIdsForAccountId(int accountId)
		{
			int[] ids = await Context
				.Characters
				.Where(c => c.AccountId == accountId)
				.Select(c => c.CharacterId)
				.ToArrayAsync();

			return ids;
		}

		public Task<CharacterEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			if(includeNavigationProperties)
				throw new NotImplementedException($"TODO: Implement nav property loading on {nameof(CharacterEntryModel)}");

			return Context
				.Characters
				.FindAsync(key);
		}

		public Task<CharacterEntryModel> RetrieveAsync(string key, bool includeNavigationProperties = false)
		{
			if(includeNavigationProperties)
				throw new NotImplementedException($"TODO: Implement nav properties for {nameof(CharacterEntryModel)}");

			return Context
				.Characters
				.FirstAsync(c => c.CharacterName == key);
		}

		public async Task<bool> TryCreateAsync(CharacterEntryModel model)
		{
			await Context.Characters.AddAsync(model);

			//Returns the amount of rows changed.
			//We expect more than 1 to indicate it was added successfully
			return 0 != await Context.SaveChangesAsync();
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			throw new NotImplementedException($"TODO: Implement delete based on key.");
		}

		public Task<bool> TryDeleteAsync(string key)
		{
			throw new NotImplementedException($"TODO: Implement delete based on key.");
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterEntryModel model)
		{
			GeneralGenericCrudRepositoryProvider<int, CharacterEntryModel> crudProvider = new GeneralGenericCrudRepositoryProvider<int, CharacterEntryModel>(Context.Characters, Context);

			return crudProvider.UpdateAsync(key, model);
		}

		/// <inheritdoc />
		public async Task UpdateAsync(string key, CharacterEntryModel model)
		{
			//Since the generic crud provider will use Find we can't use it
			//with are secondary name key. We have to implement this manually
			if(!await Context.Characters.AnyAsync(c => c.CharacterName == key).ConfigureAwait(false))
				throw new InvalidOperationException($"Cannot update model with Key: {key} as it does not exist.");

			Context.Characters.Update(model);

			await Context.SaveChangesAsync()
				.ConfigureAwait(false);
		}
	}
}
