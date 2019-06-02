using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	/// <summary>
	/// Base class type for all repositories that want to
	/// delegate the generic CRUD methods to a general crud repository.
	/// </summary>
	/// <typeparam name="TContextType">The database context type.</typeparam>
	/// <typeparam name="TKey">The primary key type.</typeparam>
	/// <typeparam name="TModel">The model type.</typeparam>
	public abstract class BaseGenericBackedDatabaseRepository<TContextType, TKey, TModel> : IGenericRepositoryCrudable<TKey, TModel>
		where TContextType : DbContext 
		where TModel : class
	{
		private IGenericRepositoryCrudable<TKey, TModel> GenericRepository { get; }

		/// <summary>
		/// The database context.
		/// </summary>
		protected TContextType Context { get; }

		/// <inheritdoc />
		public BaseGenericBackedDatabaseRepository([JetBrains.Annotations.NotNull] TContextType context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));

			GenericRepository = new GeneralGenericCrudRepositoryProvider<TKey, TModel>(context.Set<TModel>(), context);
		}

		/// <inheritdoc />
		public virtual Task<bool> ContainsAsync(TKey key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public virtual Task<bool> TryCreateAsync(TModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public virtual Task<TModel> RetrieveAsync(TKey key)
		{
			return GenericRepository.RetrieveAsync(key);
		}

		/// <inheritdoc />
		public virtual Task<bool> TryDeleteAsync(TKey key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public virtual Task UpdateAsync(TKey key, TModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}
	}
}
