using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//TODO: Refactor to generic repository.
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
			return await Context.ZoneEntries.FindAsync(key).ConfigureAwaitFalse() != null;
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
		public Task<ZoneInstanceEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			if(includeNavigationProperties)
				throw new NotImplementedException($"TODO: Add nav property support for {nameof(ZoneInstanceEntryModel)}");

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

		public async Task<ZoneInstanceEntryModel> FindFirstWithWorldId(long worldId)
		{
			return await Context.ZoneEntries
				.FirstOrDefaultAsync(z => z.WorldId == worldId);
		}

		public async Task CleanupExpiredZonesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			//Can't rely on IsExpired property in LINQ EF.
			long currentTickTime = DateTime.UtcNow.Ticks;
			long expirationTimeLength = ZoneInstanceEntryModel.ExpirationTimeLength;

			//TODO: We can reduce the memory we're loading here most likely.
			//Query all expired zoneid
			var expiredZoneModels = await Context.ZoneEntries
				.Where(z => (currentTickTime - z.LastCheckinTime) >= expirationTimeLength)
				.ToArrayAsync(cancellationToken);

			Context.ZoneEntries.RemoveRange(expiredZoneModels);
			await Context.SaveChangesAsync(cancellationToken);
		}

		public Task<ZoneInstanceEntryModel> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Context.ZoneEntries.FirstOrDefaultAsync(cancellationToken);
		}

		public void Dispose()
		{
			Context?.Dispose();
		}
	}
}
