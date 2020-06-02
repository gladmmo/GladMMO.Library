using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;

namespace GladMMO
{
	public interface IClientDataCollectionContainer
	{
		/// <summary>
		/// Await this if you're unsure data has finished loading.
		/// </summary>
		Task DataLoadingTask { get; }

		IGDBCCollection<TEntryType> DataType<TEntryType>()
			where TEntryType : IDBCEntryIdentifiable;
	}

	public static class IClientDataCollectionContainerExtensions
	{
		/// <summary>
		/// Attempts to retrieve the entry with id <see cref="key"/>.
		/// Looking for DBC entry type specified by the generic parameter.
		/// </summary>
		/// <typeparam name="TEntryType">The DBC entry type.</typeparam>
		/// <param name="collection">Data collection.</param>
		/// <param name="key">Entry id.</param>
		/// <returns>The DBC entry.</returns>
		public static TEntryType GetEntry<TEntryType>([NotNull] this IClientDataCollectionContainer collection, int key)
			where TEntryType : IDBCEntryIdentifiable
		{
			if(collection == null) throw new ArgumentNullException(nameof(collection));

			return collection.DataType<TEntryType>()[key];
		}

		/// <summary>
		/// Attempts to retrieve the entry with id <see cref="key"/>.
		/// Looking for DBC entry type specified by the generic parameter.
		/// If not found it throws an exception.
		/// </summary>
		/// <typeparam name="TEntryType">The DBC entry type.</typeparam>
		/// <param name="collection">Data collection.</param>
		/// <param name="key">Entry id.</param>
		/// <exception cref="KeyNotFoundException">Throws if the specified <see cref="key"/> was not found.</exception>
		/// <returns>The DBC entry.</returns>
		public static TEntryType AssertEntry<TEntryType>([NotNull] this IClientDataCollectionContainer collection, int key)
			where TEntryType : IDBCEntryIdentifiable
		{
			if(collection == null) throw new ArgumentNullException(nameof(collection));

			if(!HasEntry<TEntryType>(collection, key))
				throw new KeyNotFoundException($"{typeof(TEntryType).Name} does not contain Key: {key}");

			return collection.DataType<TEntryType>()[key];
		}

		/// <summary>
		/// Indicates if an entry exists with the id <see cref="key"/>.
		/// Looking for DBC entry type specified by the generic parameter.
		/// </summary>
		/// <typeparam name="TEntryType">The DBC entry type.</typeparam>
		/// <param name="collection">Data collection.</param>
		/// <param name="key">Entry id.</param>
		/// <returns>The DBC entry.</returns>
		public static bool HasEntry<TEntryType>([NotNull] this IClientDataCollectionContainer collection, int key) 
			where TEntryType : IDBCEntryIdentifiable
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));

			return collection
				.DataType<TEntryType>()
				.ContainsKey(key);
		}
	}
}
