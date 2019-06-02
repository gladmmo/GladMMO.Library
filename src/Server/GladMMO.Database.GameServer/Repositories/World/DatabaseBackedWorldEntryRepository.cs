using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DatabaseBackedWorldEntryRepository : IWorldEntryRepository
	{
		private IGenericRepositoryCrudable<long, WorldEntryModel> DefaultRepository { get; }

		private ContentDatabaseContext DatabaseContext { get; }

		/// <inheritdoc />
		public DatabaseBackedWorldEntryRepository(ContentDatabaseContext databaseContext)
		{
			DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
			DefaultRepository = new GeneralGenericCrudRepositoryProvider<long, WorldEntryModel>(databaseContext.Worlds, databaseContext);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(long key)
		{
			return DefaultRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(WorldEntryModel model)
		{
			return DefaultRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<WorldEntryModel> RetrieveAsync(long key)
		{
			return DefaultRepository.RetrieveAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(long key)
		{
			return DefaultRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(long key, WorldEntryModel model)
		{
			return DefaultRepository.UpdateAsync(key, model);
		}

		/// <inheritdoc />
		public async Task SetWorldValidated(long worldId)
		{
			WorldEntryModel model = await this.DefaultRepository.RetrieveAsync(worldId)
				.ConfigureAwait(false);

			model.IsValidated = true;

			await DefaultRepository.UpdateAsync(worldId, model)
				.ConfigureAwait(false);
		}
	}
}
