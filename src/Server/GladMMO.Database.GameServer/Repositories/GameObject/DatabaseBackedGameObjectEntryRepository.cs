using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGameObjectEntryRepository : IGameObjectEntryRepository
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, GameObjectEntryModel> GenericRepository { get; }

		public DatabaseBackedGameObjectEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, GameObjectEntryModel>(context.GameObjects, context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(GameObjectEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<GameObjectEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, GameObjectEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		public async Task<IReadOnlyCollection<GameObjectEntryModel>> RetrieveAllWorldIdAsync(int worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			return await Context.GameObjects
				.Where(c => c.WorldId == worldId)
				.ToArrayAsync();
		}
	}
}
