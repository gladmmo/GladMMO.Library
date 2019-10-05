using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedWorldTeleporterEntryRepository : IWorldTeleporterGameObjectEntryRepository
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, GameObjectWorldTeleporterEntryModel> GenericRepository { get; }

		public DatabaseBackedWorldTeleporterEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, GameObjectWorldTeleporterEntryModel>(context.WorldTeleporters, context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(GameObjectWorldTeleporterEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<GameObjectWorldTeleporterEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, GameObjectWorldTeleporterEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		//TODO: This is not exposed anywhere
		public async Task<IReadOnlyCollection<GameObjectWorldTeleporterEntryModel>> RetrieveAllWorldIdAsync(int worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			return await Context.WorldTeleporters
				.Include(wt => wt.TargetGameObject)
				.Where(wt => wt.TargetGameObject.WorldId == worldId)
				.ToArrayAsync();
		}
	}
}
