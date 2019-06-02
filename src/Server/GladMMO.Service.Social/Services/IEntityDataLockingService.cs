using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IEntityDataLockingService
	{
		/// <summary>
		/// Releases one interest unit for the provided
		/// entity guid <see cref="guid"/>.
		/// </summary>
		/// <param name="guid">The guid to release interest for.</param>
		/// <returns></returns>
		Task ReleaseEntityInterestAsync(NetworkEntityGuid guid);

		/// <summary>
		/// Aquires an increase of one interest unit for the provided
		/// entity guid <see cref="guid"/>.
		/// </summary>
		/// <param name="guid">The guid to increase interest for.</param>
		/// <returns></returns>
		Task RegisterEntityInterestAsync(NetworkEntityGuid guid);

		/// <summary>
		/// Indicates if the entity data locking service
		/// has a registeration for the provided entity <see cref="guid"/>.
		/// </summary>
		/// <param name="guid"></param>
		/// <returns>True if a lock service is available for the entity.</returns>
		Task<bool> ContainsLockingServiceForAsync(NetworkEntityGuid guid);

		/// <summary>
		/// Attempts to aquire a lock for the provided <see cref="guid"/> <see cref="NetworkEntityGuid"/>.
		/// This is not object instance based. It is on internal value basis.
		/// </summary>
		/// <param name="guid">The Entity to lock for.</param>
		/// <returns>An awaitable and dispoable async lock object.</returns>
		Task<IDisposable> AquireEntityLockAsync(NetworkEntityGuid guid);

		/// <summary>
		/// Attempts to aquire the final lock for the entity <see cref="guid"/>.
		/// What this means, is it will lock on the entity IF and ONLY IF there remains
		/// one final interest unit for the entity. Additionally on disposal of the lock it will clear up the remaining and last lock for the entity
		/// itself.
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		Task<FinalEntityLockResult> TryAquireFinalEntityLockAsync(NetworkEntityGuid guid);
	}

	//TODO: Move somewhere
	public sealed class FinalEntityLockResult
	{
		public bool Result => LockObject != null;

		public IDisposable LockObject { get; }

		/// <inheritdoc />
		public FinalEntityLockResult(IDisposable lockObject)
		{
			LockObject = lockObject;
		}

		/// <summary>
		/// Failed lock result.
		/// </summary>
		public FinalEntityLockResult()
		{
			
		}
	}
}
