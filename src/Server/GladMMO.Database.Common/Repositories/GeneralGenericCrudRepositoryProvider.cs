using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public class GeneralGenericCrudRepositoryProvider<TKey, TModelType> : IGenericRepositoryCrudable<TKey, TModelType> 
		where TModelType : class
	{
		protected DbSet<TModelType> ModelSet { get; }

		protected DbContext Context { get; }

		/// <inheritdoc />
		public GeneralGenericCrudRepositoryProvider(DbSet<TModelType> modelSet, DbContext context)
		{
			ModelSet = modelSet ?? throw new ArgumentNullException(nameof(modelSet));
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <inheritdoc />
		public async Task<bool> ContainsAsync(TKey key)
		{
			return await RetrieveAsync(key).ConfigureAwait(false) != null;
		}

		/// <inheritdoc />
		public async Task<bool> TryCreateAsync(TModelType model)
		{
			//TODO: Should we validate no key already exists?
			ModelSet.Add(model);
			return await SaveAndCheckResultsAsync()
				.ConfigureAwait(false);
		}

		private async Task<bool> SaveAndCheckResultsAsync()
		{
			return await Context.SaveChangesAsync().ConfigureAwait(false) != 0;
		}

		/// <inheritdoc />
		public virtual async Task<TModelType> RetrieveAsync(TKey key, bool includeNavigationProperties = false)
		{
			if (includeNavigationProperties)
			{
				TModelType model = await RetrieveAsync(key, false);

				foreach(var navigation in Context.Entry(model).Navigations)
				{
					navigation.Load();
				}

				return model;
			}
			else
				return await ModelSet.FindAsync(key);
		}

		/// <inheritdoc />
		public async Task<bool> TryDeleteAsync(TKey key)
		{
			//If it doesn't exist then this will just fail, so get out soon.
			if(!await ContainsAsync(key).ConfigureAwait(false))
				return false;

			TModelType modelType = await RetrieveAsync(key)
				.ConfigureAwait(false);

			ModelSet.Remove(modelType);

			return await SaveAndCheckResultsAsync()
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task UpdateAsync(TKey key, TModelType model)
		{
			if(!await ContainsAsync(key).ConfigureAwait(false))
				throw new InvalidOperationException($"Cannot update model with Key: {key} as it does not exist.");

			//TODO: is this slow? Is there a better way to deal with tracked entities?
			Context.Entry(await RetrieveAsync(key).ConfigureAwait(false)).State = EntityState.Detached;

			ModelSet.Update(model);

			await SaveAndCheckResultsAsync()
				.ConfigureAwait(false);
		}
	}
}
