using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using FreecraftCore.Serializer;

namespace GladMMO
{
	public sealed class DefaultClientDataCollectionContainer : IClientDataCollectionContainer
	{
		private ISerializerService Serializer { get; }

		public IKeyedClientDataCollection<MapEntry<StringDBCReference<MapEntry<string>>>> MapEntry { get; private set; }

		public DefaultClientDataCollectionContainer([NotNull] ISerializerService serializer)
		{
			Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
		}

		public async Task LoadDataAsync()
		{
			//Only ever load once, even if it didn't fully finish loading.
			if (MapEntry != null)
				return;

			MapEntry = await CreateClientDataCollection<MapEntry<StringDBCReference<MapEntry<string>>>>();
		}

		private async Task<DefaultKeyedClientDataCollection<T>> CreateClientDataCollection<T>() 
			where T : IDBCEntryIdentifiable
		{
			return new DefaultKeyedClientDataCollection<T>(await LoadFileAsync<T>());
		}

		private async Task<IReadOnlyDictionary<int, T>> LoadFileAsync<T>() 
			where T : IDBCEntryIdentifiable
		{
			using (FileStream stream = new FileStream($"DBC/{typeof(T).GetGenericTypeDefinition().Name}", FileMode.Open, FileAccess.Read))
			{
				DBCEntryReader<T> reader = new DBCEntryReader<T>(stream, Serializer);
				ParsedDBCFile<T> file = await reader.Parse();

				return file.RecordDatabase;
			}
		}
	}
}
