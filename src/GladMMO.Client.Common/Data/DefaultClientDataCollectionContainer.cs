using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore;
using FreecraftCore.Serializer;

namespace GladMMO
{
	internal static class GDBCCollectionExtensions
	{
		public static async Task AddToDictionary<T>(this Task<GDBCCollection<T>> collection, IDictionary<Type, object> map)
			where T : IDBCEntryIdentifiable
		{
			map.Add(typeof(T), await collection.ConfigureAwaitFalse());
			UnityEngine.Debug.Log($"Finished loading DBC {typeof(T).Name} on Thread: {Thread.CurrentThread.ManagedThreadId}");
		}
	}

	public sealed class DefaultClientDataCollectionContainer : IClientDataCollectionContainer
	{
		internal static Type[] DBCTypes = new[]
		{
			typeof(MapEntry<string>),
			typeof(LoadingScreensEntry<string>),
			typeof(SpellIconEntry<string>),
			typeof(SpellEntry<string>)
		};

		internal static IEnumerable<Type> DBCCollectionTypes
		{
			get
			{
				foreach (Type t in DBCTypes)
					yield return typeof(GDBCCollection<>).MakeGenericType(t);
			}
		}

		private ISerializerService Serializer { get; }

		public Task DataLoadingTask { get; private set; }

		//This is a TOTAL hack. It's upcasting GDBC collections to object so we can store and map them
		//for pseudo-generic access.
		private Dictionary<Type, object> GDBCCollectionMap { get; } = new Dictionary<Type, object>();

		public DefaultClientDataCollectionContainer([NotNull] ISerializerService serializer)
		{
			Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

			if(!serializer.isCompiled)
				throw new InvalidOperationException($"Serializer not compiled.");
		}

		public void StartLoadingAsync()
		{
			//Only ever load once, even if it didn't fully finish loading.
			if (GDBCCollectionMap.Count > 1)
				return;

			DataLoadingTask = CreateDataLoadingTask();
		}

		private Task CreateDataLoadingTask()
		{
			List<Task> loadingTaskList = new List<Task>
			{
				//ALWAYS start this one first.
				LoadOnBackgroundThreadAsync<SpellEntry<string>>(),
				LoadOnBackgroundThreadAsync<MapEntry<string>>(), 
				LoadOnBackgroundThreadAsync<LoadingScreensEntry<string>>(), 
				LoadOnBackgroundThreadAsync<SpellIconEntry<string>>(), 
			};

			return Task.WhenAll(loadingTaskList);
		}

		private Task LoadOnBackgroundThreadAsync<T>() 
			where T : IDBCEntryIdentifiable
		{
			return LoadFileAsync<T>().AddToDictionary(GDBCCollectionMap);
		}

		public IGDBCCollection<TEntryType> DataType<TEntryType>()
			where TEntryType : IDBCEntryIdentifiable
		{
			if(!GDBCCollectionMap.ContainsKey(typeof(TEntryType)))
				throw new InvalidOperationException($"Tried to load DBC: {typeof(TEntryType).Name} but DBC is not loaded. Add to DBC Array: {nameof(DBCTypes)}.");

			return (IGDBCCollection<TEntryType>)GDBCCollectionMap[typeof(TEntryType)];
		}

		private async Task<GDBCCollection<T>> LoadFileAsync<T>() 
			where T : IDBCEntryIdentifiable
		{
			if (!DBCTypes.Contains(typeof(T)))
				throw new InvalidOperationException($"Tried to load DBC: {typeof(T).Name} but DBC is not specified in Known DBC Array: {nameof(DBCTypes)}");

			string dbcName = typeof(T).GetGenericTypeDefinition().Name.Substring(0, typeof(T).GetGenericTypeDefinition().Name.LastIndexOf("Entry"));
			string path = $"GDBC/{dbcName}.gdbc";

			try
			{

				using (MemoryStream ms = new MemoryStream())
				{
					using(FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
					{
						await stream.CopyToAsync(ms)
							.ConfigureAwaitFalseVoid();
						ms.Position = 0;
					}

					return Serializer.Deserialize<GDBCCollection<T>>(new DefaultStreamReaderStrategy(ms));
				}
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Failed to load Client Data. Path: {path} Reason: {e.Message}", e);
			}
		}
	}
}
