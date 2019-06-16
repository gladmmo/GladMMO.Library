using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DatabaseBackedCreatureEntryRepository : ICreatureEntryRepository
	{
		private IGenericRepositoryCrudable<int, CreatureEntryModel> GenericRepository { get; }

		public DatabaseBackedCreatureEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, CreatureEntryModel>(context.Creatures, context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(CreatureEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<CreatureEntryModel> RetrieveAsync(int key)
		{
			return GenericRepository.RetrieveAsync(key);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, CreatureEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		public Task<IReadOnlyCollection<CreatureEntryModel>> RetrieveAllWithMapIdAsync(int mapId)
		{
			throw new NotImplementedException($"TODO: Reimplement map querying.");
		}
	}
}
