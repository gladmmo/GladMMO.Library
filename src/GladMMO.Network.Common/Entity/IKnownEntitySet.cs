using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Nito.AsyncEx;

namespace GladMMO
{
	public interface IReadonlyKnownEntitySet : IEnumerable<ObjectGuid>
	{
		/// <summary>
		/// Indicates if the entity is a known entity.
		/// </summary>
		/// <param name="guid">The guid to check</param>
		/// <returns>True if known.</returns>
		bool isEntityKnown(ObjectGuid guid);

		/// <summary>
		/// The synchronization object for the known entity set.
		/// </summary>
		ReaderWriterLockSlim LockObject { get; }
	}

	public interface IKnownEntitySet : IReadonlyKnownEntitySet
	{
		/// <summary>
		/// Removed entity if it's known.
		/// Throws if not known.
		/// </summary>
		/// <param name="guid">The entity to removed.</param>
		void RemoveEntity(ObjectGuid guid);

		/// <summary>
		/// Adds the entity if it's not known.
		/// Throws if already known.
		/// </summary>
		/// <param name="guid"></param>
		void AddEntity(ObjectGuid guid);
	}
}
