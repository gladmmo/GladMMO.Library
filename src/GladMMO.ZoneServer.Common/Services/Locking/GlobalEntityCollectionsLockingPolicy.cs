using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class GlobalEntityCollectionsLockingPolicy : IContextualResourceLockingPolicy<object> //nothing to use as the context for locking
	{
		private static AsyncReaderWriterLock GlobalCollectionLock { get; }

		//There should only ever be 1 lock, even if there are multiple of this
		static GlobalEntityCollectionsLockingPolicy()
		{
			GlobalCollectionLock = new AsyncReaderWriterLock();
		}

		/// <inheritdoc />
		public IDisposable ReaderLock(object context, CancellationToken cancellationToken)
		{
			return GlobalCollectionLock.ReaderLock(cancellationToken);
		}

		/// <inheritdoc />
		public AwaitableDisposable<IDisposable> ReaderLockAsync(object context, CancellationToken cancellationToken)
		{
			return GlobalCollectionLock.ReaderLockAsync(cancellationToken);
		}

		/// <inheritdoc />
		public IDisposable WriterLock(object context, CancellationToken cancellationToken)
		{
			return GlobalCollectionLock.WriterLock(cancellationToken);
		}

		/// <inheritdoc />
		public AwaitableDisposable<IDisposable> WriterLockAsync(object context, CancellationToken cancellationToken)
		{
			return GlobalCollectionLock.WriterLockAsync(cancellationToken);
		}
	}
}
