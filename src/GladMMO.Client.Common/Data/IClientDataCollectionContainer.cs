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
}
