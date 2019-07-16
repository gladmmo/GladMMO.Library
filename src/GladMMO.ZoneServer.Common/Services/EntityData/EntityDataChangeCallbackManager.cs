using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	//TODO: We need some handling for callback cleanup, especially when an entity disappears.
	public sealed class EntityDataChangeCallbackManager : IEntityDataChangeCallbackRegisterable, IEntityDataChangeCallbackService, IEntityCollectionRemovable
	{
		private Dictionary<NetworkEntityGuid, Dictionary<int, Action<int>>> CallbackMap { get; }

		public EntityDataChangeCallbackManager()
		{
			CallbackMap = new Dictionary<NetworkEntityGuid, Dictionary<int, Action<int>>>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);
		}

		/// <inheritdoc />
		public IEntityDataEventUnregisterable RegisterCallback<TCallbackValueCastType>(NetworkEntityGuid entity, int dataField, Action<NetworkEntityGuid, EntityDataChangedArgs<TCallbackValueCastType>> callback) 
			where TCallbackValueCastType : struct
		{
			//TODO: Anyway we can avoid this for registering callbacks, wasted cycles kinda
			if(!CallbackMap.ContainsKey(entity))
				CallbackMap.Add(entity, new Dictionary<int, Action<int>>());

			//TODO: This isn't thread safe, this whole thinjg isn't. That could be problematic
			Action<int> dataChangeEvent = newValue =>
			{
				//TODO: If we ever support original value we should change this
				//So, the callback needs to send the entity guid and the entity data change args which contain the original (not working yet) and new value.
				callback(entity, new EntityDataChangedArgs<TCallbackValueCastType>(default(TCallbackValueCastType), Unsafe.As<int, TCallbackValueCastType>(ref newValue)));
			};

			//We need to add a null action here or it will throw when we try to add the action. But if one exists we need to Delegate.Combine
			if(!CallbackMap[entity].ContainsKey(dataField))
				CallbackMap[entity].Add(dataField, dataChangeEvent);
			else
				CallbackMap[entity][dataField] += dataChangeEvent;

			return new DefaultEntityDataEventUnregisterable(() => CallbackMap[entity][dataField] -= dataChangeEvent);
		}

		/// <inheritdoc />
		public void InvokeChangeEvents(NetworkEntityGuid entity, int field, int newValueAsInt)
		{
			//We aren't watching ANY data changes for this particular entity.
			if(!CallbackMap.ContainsKey(entity))
				return;

			//If we have any registered callbacks for this entity's data change we should dispatch it (they will all be called)
			if(CallbackMap[entity].ContainsKey(field))
				CallbackMap[entity][field]?.Invoke(newValueAsInt);
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			return CallbackMap.Remove(entityGuid);
		}
	}
}
