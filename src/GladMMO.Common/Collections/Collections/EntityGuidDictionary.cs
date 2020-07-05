using System; using FreecraftCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Generic dictionary with <see cref="ObjectGuid"/> key types.
	/// </summary>
	/// <typeparam name="TValue">Value type.</typeparam>
	public class EntityGuidDictionary<TValue> : Glader.Essentials.EntityGuidDictionary<ObjectGuid, TValue>, IReadonlyEntityGuidMappable<TValue>, IEntityGuidMappable<TValue>, IEntityCollectionRemovable
	{
		//TODO: Some versions of Unity don't support Recurision??
		public ReaderWriterLockSlim SyncObj { get; } = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public EntityGuidDictionary()
			: base(ObjectGuidEqualityComparer<ObjectGuid>.Instance)
		{

		}

		public new IEnumerator<TValue> GetEnumerator()
		{
			return base.Values.GetEnumerator();
		}
	}
}
