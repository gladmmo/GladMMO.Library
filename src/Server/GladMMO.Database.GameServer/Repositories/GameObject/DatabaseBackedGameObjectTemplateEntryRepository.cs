using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGameObjectTemplateEntryRepository : IGameObjectTemplateRepository
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, GameObjectTemplateEntryModel> GenericRepository { get; }

		public DatabaseBackedGameObjectTemplateEntryRepository([NotNull] ContentDatabaseContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, GameObjectTemplateEntryModel>(context.Set<GameObjectTemplateEntryModel>(), context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(GameObjectTemplateEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<GameObjectTemplateEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, GameObjectTemplateEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		public async Task<string> RetrieveNameAsync(int key)
		{
			GameObjectTemplateEntryModel findAsync = await Context.GameObjectTemplates
				.FindAsync(key);

			return findAsync.GameObjectName;
		}

		public async Task<IReadOnlyCollection<GameObjectTemplateEntryModel>> RetrieveTemplatesByWorldIdAsync(int worldId)
		{
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			return await Context.GameObjects
				.Where(c => c.WorldId == worldId)
				.Include(c => c.GameObjectTemplate)
				.Select(c => c.GameObjectTemplate)
				.ToArrayAsync();
		}
	}
}
