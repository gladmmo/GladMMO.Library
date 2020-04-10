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

		public Task DataLoadingTask { get; private set; }

		public IKeyedClientDataCollection<MapEntry<StringDBCReference<MapEntry<string>>>> MapEntry { get; private set; }

		public DefaultClientDataCollectionContainer([NotNull] ISerializerService serializer)
		{
			Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
		}

		public void StartLoadingAsync()
		{
			//Only ever load once, even if it didn't fully finish loading.
			if (MapEntry != null && DataLoadingTask == null)
				return;

			DataLoadingTask = CreateDataLoadingTask();
		}

		private async Task CreateDataLoadingTask()
		{
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

			string dbcName = typeof(T).GetGenericTypeDefinition().Name.Substring(0, typeof(T).GetGenericTypeDefinition().Name.LastIndexOf("Entry"));
			string path = $"DBC/{dbcName}.dbc";

			try
			{
				using(FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					DBCEntryReader<T> reader = new DBCEntryReader<T>(stream, Serializer);
					ParsedDBCFile<T> file = await reader.Parse();

					//Only generic types support strings
					if(typeof(T).IsGenericType)
					{
						stream.Position = 0;

						DbcStringReader stringReader = new DbcStringReader(stream, Serializer);
						ClientDataCollectionExtensions.InternalStringReferenceMap.Add(typeof(T).GenericTypeArguments[0].GenericTypeArguments[0], await stringReader.ParseOnlyStrings());
					}

					return file.RecordDatabase;
				}
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Failed to load Client Data. Path: {path} Reason: {e.Message}", e);
			}
		}

		//TODO: move to static class
		public static string GetFriendlyName(Type type)
		{
			string friendlyName = type.Name;
			if(type.IsGenericType)
			{
				int iBacktick = friendlyName.IndexOf('`');
				if(iBacktick > 0)
				{
					friendlyName = friendlyName.Remove(iBacktick);
				}
				friendlyName += "<";
				Type[] typeParameters = type.GetGenericArguments();
				for(int i = 0; i < typeParameters.Length; ++i)
				{
					string typeParamName = GetFriendlyName(typeParameters[i]);
					friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
				}
				friendlyName += ">";
			}

			return friendlyName;
		}
	}
}
