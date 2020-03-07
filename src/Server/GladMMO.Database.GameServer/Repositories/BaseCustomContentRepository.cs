using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public abstract class BaseCustomContentRepository<TCustomContentModelType> : ICustomContentRepository<TCustomContentModelType>
		where TCustomContentModelType : class, IClientContentPersistable
	{
		private IGenericRepositoryCrudable<long, TCustomContentModelType> DefaultRepository { get; }

		protected ContentDatabaseContext DatabaseContext { get; }

		/// <inheritdoc />
		protected BaseCustomContentRepository(ContentDatabaseContext databaseContext)
		{
			DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
			DefaultRepository = new GeneralGenericCrudRepositoryProvider<long, TCustomContentModelType>(databaseContext.Set<TCustomContentModelType>(), databaseContext);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(long key)
		{
			return DefaultRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(TCustomContentModelType model)
		{
			return DefaultRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<TCustomContentModelType> RetrieveAsync(long key, bool includeNavigationProperties = false)
		{
			return DefaultRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(long key)
		{
			return DefaultRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(long key, TCustomContentModelType model)
		{
			return DefaultRepository.UpdateAsync(key, model);
		}
	}
}
