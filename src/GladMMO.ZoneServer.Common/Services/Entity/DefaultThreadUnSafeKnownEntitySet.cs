using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class DefaultThreadUnSafeKnownEntitySet : IKnownEntitySet, IReadonlyKnownEntitySet
	{
		private HashSet<NetworkEntityGuid> InternalKnownSet { get; }

		public AsyncReaderWriterLock LockObject { get; }

		/// <inheritdoc />
		public DefaultThreadUnSafeKnownEntitySet()
		{
			InternalKnownSet = new HashSet<NetworkEntityGuid>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);
			LockObject = new AsyncReaderWriterLock();
		}

		/// <inheritdoc />
		public void RemoveEntity(NetworkEntityGuid guid)
		{
			if(!isEntityKnown(guid))
				throw new InvalidOperationException($"Cannot removed EntityGuid: {guid} because does not exists in Set: {nameof(DefaultThreadUnSafeKnownEntitySet)}.");

			InternalKnownSet.Remove(guid);
		}

		/// <inheritdoc />
		public void AddEntity(NetworkEntityGuid guid)
		{
			if(isEntityKnown(guid))
				throw new InvalidOperationException($"Cannot add EntityGuid: {guid} because it already exists in Set: {nameof(DefaultThreadUnSafeKnownEntitySet)}.");

			InternalKnownSet.Add(guid);
		}

		/// <inheritdoc />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool isEntityKnown(NetworkEntityGuid guid)
		{
			return InternalKnownSet.Contains(guid);
		}

		/// <inheritdoc />
		public IEnumerator<NetworkEntityGuid> GetEnumerator()
		{
			return InternalKnownSet.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
