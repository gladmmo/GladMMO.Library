using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DatabaseBackedAvatarEntryRepository : IAvatarEntryRepository
	{
		private IGenericRepositoryCrudable<long, AvatarEntryModel> DefaultRepository { get; }

		private ContentDatabaseContext DatabaseContext { get; }

		/// <inheritdoc />
		public DatabaseBackedAvatarEntryRepository(ContentDatabaseContext databaseContext)
		{
			DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
			DefaultRepository = new GeneralGenericCrudRepositoryProvider<long, AvatarEntryModel>(databaseContext.Avatars, databaseContext);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(long key)
		{
			return DefaultRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(AvatarEntryModel model)
		{
			return DefaultRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<AvatarEntryModel> RetrieveAsync(long key, bool includeNavigationProperties = false)
		{
			return DefaultRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(long key)
		{
			return DefaultRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(long key, AvatarEntryModel model)
		{
			return DefaultRepository.UpdateAsync(key, model);
		}

		/// <inheritdoc />
		public async Task SetWorldValidated(long worldId)
		{
			AvatarEntryModel model = await this.DefaultRepository.RetrieveAsync(worldId)
				.ConfigureAwaitFalse();

			model.IsValidated = true;

			await DefaultRepository.UpdateAsync(worldId, model)
				.ConfigureAwaitFalseVoid();
		}
	}
}
