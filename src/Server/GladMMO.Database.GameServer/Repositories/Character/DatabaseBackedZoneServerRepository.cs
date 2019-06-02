using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedZoneServerRepository : IZoneServerRepository
	{
		private CharacterDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedZoneServerRepository(CharacterDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <inheritdoc />
		public async Task<bool> ContainsAsync(int key)
		{
			return await Context.ZoneEntries.FindAsync(key).ConfigureAwait(false) != null;
		}

		/// <inheritdoc />
		public async Task<bool> TryCreateAsync(ZoneInstanceEntryModel model)
		{
#pragma warning disable AsyncFixer02 // Long running or blocking operations under an async method
			Context
				.ZoneEntries
				.Add(model);
#pragma warning restore AsyncFixer02 // Long running or blocking operations under an async method

			return 0 != await Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public Task<ZoneInstanceEntryModel> RetrieveAsync(int key)
		{
			return Context
				.ZoneEntries
				.FindAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, ZoneInstanceEntryModel model)
		{
			GeneralGenericCrudRepositoryProvider<int, ZoneInstanceEntryModel> crudProvider = new GeneralGenericCrudRepositoryProvider<int, ZoneInstanceEntryModel>(Context.ZoneEntries, Context);

			return crudProvider.UpdateAsync(key, model);
		}
	}
}
