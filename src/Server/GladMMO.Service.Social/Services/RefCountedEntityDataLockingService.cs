using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace GladMMO
{
	//TODO: We need to exame deadlock potential here.
	public class RefCountedEntityDataLockingService : IEntityDataLockingService
	{
		private AsyncReaderWriterLock InternalGlobalLock { get; }

		private ConcurrentDictionary<ulong, AsyncReaderWriterLock> EntityLockingObjectMap { get; }

		private ConcurrentDictionary<ulong, int> EntityRefCountingMap { get; }

		/// <inheritdoc />
		public RefCountedEntityDataLockingService()
		{
			EntityLockingObjectMap = new ConcurrentDictionary<ulong, AsyncReaderWriterLock>();
			EntityRefCountingMap = new ConcurrentDictionary<ulong, int>();

			InternalGlobalLock = new AsyncReaderWriterLock();
		}

		/// <inheritdoc />
		public async Task ReleaseEntityInterestAsync(NetworkEntityGuid guid)
		{
			using(await InternalGlobalLock.WriterLockAsync().ConfigureAwait(false))
			{
				if(EntityRefCountingMap.ContainsKey(guid.RawGuidValue))
				{
					EntityRefCountingMap[guid.RawGuidValue]--;

					if(EntityRefCountingMap[guid.RawGuidValue] <= 0)
					{
						EntityLockingObjectMap.Remove(guid.RawGuidValue, out var locker);
						EntityRefCountingMap.Remove(guid.RawGuidValue, out int val);
					}
				}

				//TODO: Should we throw if they try when it doesn't even know the entity?
				return;
			}
		}

		/// <inheritdoc />
		public async Task RegisterEntityInterestAsync(NetworkEntityGuid guid)
		{
			using(await InternalGlobalLock.WriterLockAsync().ConfigureAwait(false))
			{
				if(EntityRefCountingMap.ContainsKey(guid.RawGuidValue))
				{
					EntityRefCountingMap[guid.RawGuidValue]++;
				}
				else
				{
					EntityRefCountingMap[guid.RawGuidValue] = 1;
					EntityLockingObjectMap[guid.RawGuidValue] = new AsyncReaderWriterLock();
				}
			}
		}

		/// <inheritdoc />
		public async Task<bool> ContainsLockingServiceForAsync(NetworkEntityGuid guid)
		{
			using(await InternalGlobalLock.ReaderLockAsync().ConfigureAwait(false))
			{
				//We don't need to check value.
				return EntityRefCountingMap.ContainsKey(guid.RawGuidValue);
			}
		}

		/// <inheritdoc />
		public async Task<IDisposable> AquireEntityLockAsync(NetworkEntityGuid guid)
		{
			//basically, root lock so nobody can change the entity state until we're done.
			//We lock read globally and then write for the entity

			IDisposable root = null;
			IDisposable child = null;

			try
			{
				//TODO: It's POSSIBLE, but unlikely that the entity was removed. Or that they didn't check?
				root = await InternalGlobalLock.ReaderLockAsync().ConfigureAwait(false);

				if(!EntityRefCountingMap.ContainsKey(guid.RawGuidValue))
					throw new InvalidOperationException($"Entity: {guid} does not exist in the locking service.");

				//TODO: Should we do a write lock?
				child = await EntityLockingObjectMap[guid.RawGuidValue].WriterLockAsync().ConfigureAwait(false);
			}
			catch(Exception)
			{
				throw;
			}
			finally
			{
				child?.Dispose();
				root?.Dispose();
			}

			//MUST dispose this to dispose the locks.
			return new AggregateDisposableLock(root, child);
		}

		/// <inheritdoc />
		public async Task<FinalEntityLockResult> TryAquireFinalEntityLockAsync(NetworkEntityGuid guid)
		{
			IDisposable root = null;
			IDisposable childLock = null;

			try
			{
				root = await InternalGlobalLock.WriterLockAsync().ConfigureAwait(false);

				if(!EntityRefCountingMap.ContainsKey(guid.RawGuidValue))
					return new FinalEntityLockResult();

				if(EntityRefCountingMap[guid.RawGuidValue] == 1)
				{
					childLock = await EntityLockingObjectMap[guid.RawGuidValue].WriterLockAsync().ConfigureAwait(false);

					//On the disposable of the lock we should clear up the entity entry.
					return new FinalEntityLockResult(new DisposableEventDispatcherDecorator(new AggregateDisposableLock(root, childLock), () =>
					{
						EntityRefCountingMap.Remove(guid.RawGuidValue, out var val1);
						EntityLockingObjectMap.Remove(guid.RawGuidValue, out var val2);
					}));
				}
			}
			catch(Exception e)
			{
				throw;
			}
			finally
			{
				childLock?.Dispose();
				root?.Dispose();
			}
			
			return new FinalEntityLockResult();
		}

		private sealed class DisposableEventDispatcherDecorator : IDisposable
		{
			private IDisposable ManagedDisposable { get; }

			private Action OnDisposed { get; }

			/// <inheritdoc />
			public DisposableEventDispatcherDecorator(IDisposable managedDisposable, [JetBrains.Annotations.NotNull] Action onDisposed)
			{
				ManagedDisposable = managedDisposable;
				OnDisposed = onDisposed ?? throw new ArgumentNullException(nameof(onDisposed));
			}

			/// <inheritdoc />
			public void Dispose()
			{
				OnDisposed?.Invoke();
				ManagedDisposable.Dispose();
			}
		}

		private class AggregateDisposableLock : IDisposable
		{
			private IDisposable RootLock { get; }

			private IDisposable ChildLock { get; }

			/// <inheritdoc />
			public AggregateDisposableLock([JetBrains.Annotations.NotNull] IDisposable rootLock, [JetBrains.Annotations.NotNull] IDisposable childLock)
			{
				RootLock = rootLock ?? throw new ArgumentNullException(nameof(rootLock));
				ChildLock = childLock ?? throw new ArgumentNullException(nameof(childLock));
			}

			/// <inheritdoc />
			public void Dispose()
			{
				//Inner most lock disposed first
				ChildLock?.Dispose();

				//Then global root lock.
				RootLock?.Dispose();
			}
		}
	}
}
