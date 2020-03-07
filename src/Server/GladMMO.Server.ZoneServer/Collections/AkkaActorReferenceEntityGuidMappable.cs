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

		public bool ContainsKey(ObjectGuid key)
		{
			return InternalActorRefDictionary.ContainsKey(key);
		}

		public void Add(ObjectGuid key, IActorRef value)
		{
			InternalActorRefDictionary.Add(key, value);
		}

		public IActorRef this[ObjectGuid key]
		{
			get => InternalActorRefDictionary[key];
			set => InternalActorRefDictionary[key] = value;
		}

		public bool TryGetValue(ObjectGuid key, out IActorRef value)
		{
			return this.InternalActorRefDictionary.TryGetValue(key, out value);
		}

		public IEnumerator<IActorRef> GetEnumerator()
		{
			return InternalActorRefDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) InternalActorRefDictionary).GetEnumerator();
		}

		public bool RemoveEntityEntry(ObjectGuid entityGuid)
		{
			//Never remove or cleanup.
			return true;
		}
	}
}
