using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class GameObjectEntityBehaviourComponentCollection : IEntityGuidMappable<BaseGameObjectEntityBehaviourComponent>, IReadonlyEntityGuidMappable<IWorldInteractable>, IEntityCollectionRemovable
	{
		protected Dictionary<NetworkEntityGuid, BaseGameObjectEntityBehaviourComponent> ComponentMap { get; }

		public GameObjectEntityBehaviourComponentCollection()
		{
			ComponentMap = new Dictionary<NetworkEntityGuid, BaseGameObjectEntityBehaviourComponent>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);
		}

		public bool ContainsKey([NotNull] NetworkEntityGuid key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			return ComponentMap.ContainsKey(key);
		}

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, IWorldInteractable>.ContainsKey([NotNull] NetworkEntityGuid key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			//If the entity is contained AND it's an interactable.
			return this.ContainsKey(key) && this[key] is IWorldInteractable;
		}

		IWorldInteractable IReadonlyEntityGuidMappable<NetworkEntityGuid, IWorldInteractable>.this[NetworkEntityGuid key] => (IWorldInteractable)this[key];

		public void Add(NetworkEntityGuid key, BaseGameObjectEntityBehaviourComponent value)
		{
			ComponentMap.Add(key, value);
		}

		public BaseGameObjectEntityBehaviourComponent this[NetworkEntityGuid key]
		{
			get => ComponentMap[key];
			set => ComponentMap[key] = value;
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			return ComponentMap.Remove(entityGuid);
		}

		IEnumerator<IWorldInteractable> IEnumerable<IWorldInteractable>.GetEnumerator()
		{
			throw new NotImplementedException($"TODO: Implement enumerator for IWorldInteractables");
		}

		public IEnumerator<BaseGameObjectEntityBehaviourComponent> GetEnumerator()
		{
			return ComponentMap.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
