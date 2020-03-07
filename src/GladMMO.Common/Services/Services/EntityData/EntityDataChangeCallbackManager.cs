using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	//TODO: We need some handling for callback cleanup, especially when an entity disappears.
	public sealed class EntityDataChangeCallbackManager : IEntityDataChangeCallbackRegisterable, IEntityDataChangeCallbackService, IEntityCollectionRemovable
	{
		private Dictionary<ObjectGuid, Dictionary<int, Action<IEntityDataFieldContainer>>> CallbackMap { get; }

		public EntityDataChangeCallbackManager()
		{
			CallbackMap = new Dictionary<ObjectGuid, Dictionary<int, Action<IEntityDataFieldContainer>>>(ObjectGuidEqualityComparer<ObjectGuid>.Instance);
		}

		/// <inheritdoc />
		public IEntityDataEventUnregisterable RegisterCallback<TCallbackValueCastType>(ObjectGuid entity, int dataField, Action<ObjectGuid, EntityDataChangedArgs<TCallbackValueCastType>> callback) 
			where TCallbackValueCastType : struct
		{
			//TODO: Anyway we can avoid this for registering callbacks, wasted cycles kinda
			if(!CallbackMap.ContainsKey(entity))
				CallbackMap.Add(entity, new Dictionary<int, Action<IEntityDataFieldContainer>>());

			//TODO: This isn't thread safe, this whole thinjg isn't. That could be problematic
			Action<IEntityDataFieldContainer> dataChangeEvent = fields =>
			{
				//TODO: If we ever support original value we should change this
				//So, the callback needs to send the entity guid and the entity data change args which contain the original (not working yet) and new value.
				callback(entity, new EntityDataChangedArgs<TCallbackValueCastType>(default(TCallbackValueCastType), fields.GetFieldValue<TCallbackValueCastType>(dataField)));
			};

			//We need to add a null action here or it will throw when we try to add the action. But if one exists we need to Delegate.Combine
			if (!CallbackMap[entity].ContainsKey(dataField))
			{
				CallbackMap[entity].Add(dataField, dataChangeEvent);
			}
			else
			{
				CallbackMap[entity][dataField] += dataChangeEvent;
			}

			return new DefaultEntityDataEventUnregisterable(() =>
			{
				CallbackMap[entity][dataField] -= dataChangeEvent;
			});
		}

		private static bool IsRequestedTypeLong<TCallbackValueCastType>() where TCallbackValueCastType : struct
		{
			return typeof(TCallbackValueCastType) == typeof(long) || typeof(TCallbackValueCastType) == typeof(ulong);
		}

		/// <inheritdoc />
		public void InvokeChangeEvents(ObjectGuid entity, IEntityDataFieldContainer fieldContainer, int field)
		{
			//We aren't watching ANY data changes for this particular entity.
			if(!CallbackMap.ContainsKey(entity))
				return;

			//If we have any registered callbacks for this entity's data change we should dispatch it (they will all be called)
			if(CallbackMap[entity].ContainsKey(field))
				CallbackMap[entity][field]?.Invoke(fieldContainer);
		}

		public bool RemoveEntityEntry(ObjectGuid entityGuid)
		{
			return CallbackMap.Remove(entityGuid);
		}
	}
}
