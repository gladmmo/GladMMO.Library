using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class CacheableEntityNameQueryable : IEntityNameQueryable
	{
		private Dictionary<ObjectGuid, string> LocalNameMap { get; } = new Dictionary<ObjectGuid, string>(NetworkGuidEqualityComparer<ObjectGuid>.Instance);

		private INameQueryService NameServiceQueryable { get; }

		private AsyncReaderWriterLock SyncObj { get; } = new AsyncReaderWriterLock();

		/// <inheritdoc />
		public CacheableEntityNameQueryable([NotNull] INameQueryService nameServiceQueryable)
		{
			NameServiceQueryable = nameServiceQueryable ?? throw new ArgumentNullException(nameof(nameServiceQueryable));
		}

		/// <inheritdoc />
		public void EnsureExists([NotNull] ObjectGuid entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));

			using(SyncObj.ReaderLock())
				if(!LocalNameMap.ContainsKey(entity))
					throw new KeyNotFoundException($"Entity: {entity} not found in {nameof(INameQueryService)}.");
		}

		/// <inheritdoc />
		public string Retrieve([NotNull] ObjectGuid entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));

			using(SyncObj.ReaderLock())
			{
				return LocalNameMap[entity];
			}
		}

		public bool Exists(ObjectGuid entity)
		{
			using (SyncObj.ReaderLock())
			{
				return LocalNameMap.ContainsKey(entity);
			}
		}

		/// <inheritdoc />
		public async Task<string> RetrieveAsync(ObjectGuid entity)
		{
			using(await SyncObj.ReaderLockAsync())
			{
				if(LocalNameMap.ContainsKey(entity))
					return LocalNameMap[entity]; //do not call Retrieve, old versions of Unity3D don't support recursive readwrite locking.
			}

			//If we're here, it wasn't contained
			var result = await QueryRemoteNameService(entity);

			if(!result.isSuccessful)
				throw new InvalidOperationException($"Failed to query name for Entity: {entity}. Result: {result.ResultCode}.");

			//Add it
			using(await SyncObj.WriterLockAsync())
			{
				//Check if some other thing already initialized it

				if(LocalNameMap.ContainsKey(entity))
					return LocalNameMap[entity]; //do not call Retrieve, old versions of Unity3D don't support recursive readwrite locking.

				return LocalNameMap[entity] = result.isSuccessful ? result.Result.EntityName : "Unknown";
			}
		}

		private async Task<ResponseModel<NameQueryResponse, NameQueryResponseCode>> QueryRemoteNameService(ObjectGuid entity)
		{
			switch (entity.TypeId)
			{
				case EntityTypeId.TYPEID_PLAYER:
					return await NameServiceQueryable.RetrievePlayerNameAsync(entity.RawGuidValue);
				case EntityTypeId.TYPEID_GAMEOBJECT:
					return await NameServiceQueryable.RetrieveGameObjectNameAsync(entity.RawGuidValue);
				case EntityTypeId.TYPEID_UNIT:
					return await NameServiceQueryable.RetrieveCreatureNameAsync(entity.RawGuidValue);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
