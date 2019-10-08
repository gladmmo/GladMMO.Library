using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public abstract class GenericDatabaseBackedGameObjectBehaviorEntryRepository<TEntryType> : IGenericRepositoryCrudable<int, TEntryType>, 
		IInstanceableWorldObjectRepository<TEntryType>
		where TEntryType : class, IGameObjectEntryLinkable
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, TEntryType> GenericRepository { get; }

		protected GenericDatabaseBackedGameObjectBehaviorEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, TEntryType>(context.Set<TEntryType>(), context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(TEntryType model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<TEntryType> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, TEntryType model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		//TODO: This is not exposed anywhere
		public async Task<IReadOnlyCollection<TEntryType>> RetrieveAllWorldIdAsync(int worldId)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			return await Context.Set<TEntryType>()
				.Include(wt => wt.TargetGameObject)
				.Where(wt => wt.TargetGameObject.WorldId == worldId)
				.ToArrayAsync();
		}
	}
}
