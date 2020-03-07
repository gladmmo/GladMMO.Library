using System; using FreecraftCore;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class DefaultThreadUnSafeKnownEntitySet : IKnownEntitySet, IReadonlyKnownEntitySet
	{
		private HashSet<ObjectGuid> InternalKnownSet { get; }

		/// <inheritdoc />
		public ReaderWriterLockSlim LockObject { get; }

		/// <inheritdoc />
		public DefaultThreadUnSafeKnownEntitySet()
		{
			InternalKnownSet = new HashSet<ObjectGuid>(ObjectGuidEqualityComparer<ObjectGuid>.Instance);
			LockObject = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
		}

		/// <inheritdoc />
		public void RemoveEntity(ObjectGuid guid)
		{
			LockObject.EnterWriteLock();
			try
			{
				if (!isEntityKnown(guid))
					throw new InvalidOperationException($"Cannot removed EntityGuid: {guid} because does not exists in Set: {nameof(DefaultThreadUnSafeKnownEntitySet)}.");

				InternalKnownSet.Remove(guid);
			}
			finally
			{
				LockObject.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		public void AddEntity(ObjectGuid guid)
		{
			LockObject.EnterWriteLock();
			try
			{
				if (isEntityKnown(guid))
					throw new InvalidOperationException($"Cannot add EntityGuid: {guid} because it already exists in Set: {nameof(DefaultThreadUnSafeKnownEntitySet)}.");

				InternalKnownSet.Add(guid);
			}
			finally
			{
				LockObject.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool isEntityKnown(ObjectGuid guid)
		{
			LockObject.EnterReadLock();
			try
			{
				return InternalKnownSet.Contains(guid);
			}
			finally
			{
				LockObject.ExitReadLock();
			}
		}

		/// <inheritdoc />
		public IEnumerator<ObjectGuid> GetEnumerator()
		{
			LockObject.EnterReadLock();
			try
			{
				foreach (var entry in InternalKnownSet)
					yield return entry;
			}
			finally
			{
				LockObject.ExitReadLock();
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
