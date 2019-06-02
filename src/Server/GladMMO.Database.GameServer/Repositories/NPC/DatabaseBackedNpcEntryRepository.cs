using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedNpcEntryRepository : INpcEntryRepository
	{
		private NpcDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedNpcEntryRepository(NpcDatabaseContext npcDatabase)
		{
			Context = npcDatabase ?? throw new ArgumentNullException(nameof(npcDatabase));
		}

		public async Task<IReadOnlyCollection<NPCEntryModel>> RetrieveAllWithMapIdAsync(int mapId)
		{
			return await Context
				.Entries
				.Where(m => m.MapId == mapId)
				.ToArrayAsync()
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(int key)
		{
			var generalCrudProvider = BuildGeneralCrudProvider();

			return generalCrudProvider.ContainsAsync(key);
		}

		private GeneralGenericCrudRepositoryProvider<int, NPCEntryModel> BuildGeneralCrudProvider()
		{
			return new GeneralGenericCrudRepositoryProvider<int, NPCEntryModel>(Context.Entries, Context);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(NPCEntryModel model)
		{
			var generalCrudProvider = BuildGeneralCrudProvider();

			return generalCrudProvider.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<NPCEntryModel> RetrieveAsync(int key)
		{
			var generalCrudProvider = BuildGeneralCrudProvider();

			return generalCrudProvider.RetrieveAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			var generalCrudProvider = BuildGeneralCrudProvider();

			return generalCrudProvider.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, NPCEntryModel model)
		{
			GeneralGenericCrudRepositoryProvider<int, NPCEntryModel> crudProvider = new GeneralGenericCrudRepositoryProvider<int, NPCEntryModel>(Context.Entries, Context);

			return crudProvider.UpdateAsync(key, model);
		}
	}
}
