using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public sealed class AkkaActorReferenceEntityGuidMappable : IEntityGuidMappable<IActorRef>
	{
		private EntityGuidDictionary<IActorRef> InternalActorRefDictionary { get; }

		public AkkaActorReferenceEntityGuidMappable()
		{
			InternalActorRefDictionary = new EntityGuidDictionary<IActorRef>();
		}

		public bool ContainsKey(NetworkEntityGuid key)
		{
			return InternalActorRefDictionary.ContainsKey(key);
		}

		public void Add(NetworkEntityGuid key, IActorRef value)
		{
			InternalActorRefDictionary.Add(key, value);
		}

		public IActorRef this[NetworkEntityGuid key]
		{
			get => InternalActorRefDictionary[key];
			set => InternalActorRefDictionary[key] = value;
		}

		public IEnumerator<IActorRef> GetEnumerator()
		{
			return InternalActorRefDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) InternalActorRefDictionary).GetEnumerator();
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			//Never remove or cleanup.
			return true;
		}
	}
}
