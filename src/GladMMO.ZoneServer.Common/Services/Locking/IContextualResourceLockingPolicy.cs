using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	/// <summary>
	/// Contract for resource locking.
	/// </summary>
	public interface IContextualResourceLockingPolicy<in TLockingContext>
	{
		//
		// Summary:
		//     Synchronously acquires the lock as a reader. Returns a disposable that releases
		//     the lock when disposed. This method may block the calling thread.
		//
		// Parameters:
		//   cancellationToken:
		//     The cancellation token used to cancel the lock. If this is already set, then
		//     this method will attempt to take the lock immediately (succeeding if the lock
		//     is currently available).
		//
		// Returns:
		//     A disposable that releases the lock when disposed.
		IDisposable ReaderLock(TLockingContext context, CancellationToken cancellationToken);
		//
		// Summary:
		//     Asynchronously acquires the lock as a reader. Returns a disposable that releases
		//     the lock when disposed.
		//
		// Parameters:
		//   cancellationToken:
		//     The cancellation token used to cancel the lock. If this is already set, then
		//     this method will attempt to take the lock immediately (succeeding if the lock
		//     is currently available).
		//
		// Returns:
		//     A disposable that releases the lock when disposed.
		AwaitableDisposable<IDisposable> ReaderLockAsync(TLockingContext context, CancellationToken cancellationToken);
		//
		// Summary:
		//     Synchronously acquires the lock as a writer. Returns a disposable that releases
		//     the lock when disposed. This method may block the calling thread.
		//
		// Parameters:
		//   cancellationToken:
		//     The cancellation token used to cancel the lock. If this is already set, then
		//     this method will attempt to take the lock immediately (succeeding if the lock
		//     is currently available).
		//
		// Returns:
		//     A disposable that releases the lock when disposed.
		IDisposable WriterLock(TLockingContext context, CancellationToken cancellationToken);
		//
		// Summary:
		//     Asynchronously acquires the lock as a writer. Returns a disposable that releases
		//     the lock when disposed.
		//
		// Parameters:
		//   cancellationToken:
		//     The cancellation token used to cancel the lock. If this is already set, then
		//     this method will attempt to take the lock immediately (succeeding if the lock
		//     is currently available).
		//
		// Returns:
		//     A disposable that releases the lock when disposed.
		AwaitableDisposable<IDisposable> WriterLockAsync(TLockingContext context, CancellationToken cancellationToken);
	}
}
