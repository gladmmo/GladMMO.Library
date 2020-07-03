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
		private Dictionary<ObjectGuid, string> LocalNameMap { get; } = new Dictionary<ObjectGuid, string>(ObjectGuidEqualityComparer<ObjectGuid>.Instance);

		private INameQueryService NameServiceQueryable { get; }

		private AsyncReaderWriterLock SyncObj { get; } = new AsyncReaderWriterLock();

		//We need this for certain things such as corpses.
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataFieldMappable { get; }

		/// <inheritdoc />
		public CacheableEntityNameQueryable([NotNull] INameQueryService nameServiceQueryable,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataFieldMappable)
		{
			NameServiceQueryable = nameServiceQueryable ?? throw new ArgumentNullException(nameof(nameServiceQueryable));
			EntityDataFieldMappable = entityDataFieldMappable ?? throw new ArgumentNullException(nameof(entityDataFieldMappable));
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

				//Check corpse too
				if (entity.TypeId == EntityTypeId.TYPEID_CORPSE)
					if (EntityDataFieldMappable.ContainsKey(entity))
					{
						CorpseNameBuilder builder = new CorpseNameBuilder(EntityDataFieldMappable.RetrieveEntity(entity), entity, LocalNameMap);

						//If we can't build it yet, then we don't wanna just return default.
						if (builder.IsBuildable())
							return LocalNameMap[entity] = builder;
					}
			}

			//If we're here, it wasn't contained
			var result = await QueryRemoteNameService(entity);

			//Add it
			using(await SyncObj.WriterLockAsync())
			{
				//Check if some other thing already initialized it
				if (LocalNameMap.ContainsKey(entity))
					return LocalNameMap[entity]; //do not call Retrieve, old versions of Unity3D don't support recursive readwrite locking.

				return LocalNameMap[entity] = ComputeNameQueryResult(entity, result);
			}
		}

		private string ComputeNameQueryResult(ObjectGuid guid, ResponseModel<NameQueryResponse, NameQueryResponseCode> result)
		{
			if (!result.isSuccessful)
				if (guid.TypeId == EntityTypeId.TYPEID_CORPSE)
					return new CorpseNameBuilder(guid, LocalNameMap);
				else
					return GladMMOCommonConstants.DEFAULT_UNKNOWN_ENTITY_NAME_STRING;

			if (guid.TypeId == EntityTypeId.TYPEID_CORPSE)
				return new CorpseNameBuilder(result.Result.EntityName);
			else
				return result.Result.EntityName;
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
				case EntityTypeId.TYPEID_CORPSE:
					if (EntityDataFieldMappable.ContainsKey(entity))
					{
						ObjectGuid corpseOwner = EntityDataFieldMappable[entity].GetEntityGuidValue(ECorpseFields.CORPSE_FIELD_OWNER);
						return await NameServiceQueryable.RetrievePlayerNameAsync(corpseOwner.RawGuidValue);
					}
					else
						throw new InvalidOperationException($"Failed to NameQuery Corpse: {entity}");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
