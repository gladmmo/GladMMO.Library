using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterLocationRepository : ICharacterLocationRepository
	{
		/// <summary>
		/// The database service.
		/// </summary>
		private CharacterDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedCharacterLocationRepository(CharacterDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <inheritdoc />
		public async Task<bool> ContainsAsync(int key)
		{
			return null != await Context
				.CharacterLocations
				.FindAsync(key);
		}

		/// <inheritdoc />
		public async Task<bool> TryCreateAsync(CharacterLocationModel model)
		{
			if(await ContainsAsync(model.CharacterId))
				throw new ArgumentException($"Tried to add duplicate Key: {model.CharacterId} to character_locations", nameof(model));

			await Context
				.CharacterLocations
				.AddAsync(model);

			int rowChangedCount = await Context.SaveChangesAsync();

			return rowChangedCount != 0;
		}

		/// <inheritdoc />
		public async Task<CharacterLocationModel> RetrieveAsync(int key)
		{
			if(!await ContainsAsync(key))
				throw new InvalidOperationException($"Tried to read Key: {key} from character_locations. Does not exist.");

			return await Context
				.CharacterLocations
				.FindAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			throw new NotSupportedException($"TODO: Implement deleting character_locations");
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterLocationModel model)
		{
			GeneralGenericCrudRepositoryProvider<int, CharacterLocationModel> crudProvider = new GeneralGenericCrudRepositoryProvider<int, CharacterLocationModel>(Context.CharacterLocations, Context);

			return crudProvider.UpdateAsync(key, model);
		}
	}
}
