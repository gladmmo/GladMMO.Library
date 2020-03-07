using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedPlayerSpawnPointEntryRepository : IPlayerSpawnPointEntryRepository
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, PlayerSpawnPointEntryModel> GenericRepository { get; }

		public DatabaseBackedPlayerSpawnPointEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, PlayerSpawnPointEntryModel>(context.PlayerSpawnPoints, context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(PlayerSpawnPointEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<PlayerSpawnPointEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, PlayerSpawnPointEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		public async Task<IReadOnlyCollection<PlayerSpawnPointEntryModel>> RetrieveAllWorldIdAsync(int worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			return await Context.PlayerSpawnPoints
				.Where(c => c.WorldId == worldId)
				.ToArrayAsync();
		}
	}
}
