using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class InterestCollection : IReadonlyInterestCollection, IEntityInterestQueueable, IEntityInterestDequeueable, IEntityInterestSet
	{
		/// <summary>
		/// Set that contains all the entites in the tile.
		/// Is a unique set.
		/// </summary>
		private readonly HashSet<NetworkEntityGuid> _ContainedEntities = new HashSet<NetworkEntityGuid>(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance);

		/// <summary>
		/// Ordered queue of leaving entites.
		/// Should be contained in <see cref="ContainedEntities"/>
		/// </summary>
		private readonly NetworkEntityGuidQueue _LeavingQueue = new NetworkEntityGuidQueue();

		/// <summary>
		/// Ordered queue of entering entites.
		/// Not contained in <see cref="ContainedEntities"/>
		/// </summary>
		private readonly NetworkEntityGuidQueue _EnteringQueue = new NetworkEntityGuidQueue();

		/// <summary>
		/// Represents the contained entites.
		/// </summary>
		public IReadOnlyCollection<NetworkEntityGuid> ContainedEntities => _ContainedEntities;

		/// <summary>
		/// The collection of enties that are queued for leaving the tile.
		/// They have not left the title if they are in this collection, so won't be in <see cref="ContainedEntities"/>.
		/// They will leave the title in the next update.
		/// </summary>
		public IReadOnlyCollection<NetworkEntityGuid> QueuedLeavingEntities => _LeavingQueue;

		/// <summary>
		/// The collection of enties that are queued for entering the tile.
		/// They have not actually joined the tile so won't be in <see cref="ContainedEntities"/>.
		/// They will join the tile in the next update.
		/// </summary>
		public IReadOnlyCollection<NetworkEntityGuid> QueuedJoiningEntities => _EnteringQueue;

		/// <inheritdoc />
		public IDequeable<NetworkEntityGuid> LeavingDequeueable => _LeavingQueue;

		/// <inheritdoc />
		public IDequeable<NetworkEntityGuid> EnteringDequeueable => _EnteringQueue;

		/// <inheritdoc />
		public void Register([NotNull] NetworkEntityGuid key, [NotNull] NetworkEntityGuid value)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));
			if(value == null) throw new ArgumentNullException(nameof(value));

			//Both key and value are the same
			_EnteringQueue.Enqueue(value);
		}

		/// <inheritdoc />
		public bool Contains([NotNull] NetworkEntityGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			return _ContainedEntities.Contains(key);
		}

		/// <inheritdoc />
		public NetworkEntityGuid Retrieve(NetworkEntityGuid key)
		{
			if(!_ContainedEntities.Contains(key))
				throw new InvalidOperationException($"Provided Key: {key} does not exist in the tile.");

			return key;
		}

		/// <inheritdoc />
		public bool Unregister([NotNull] NetworkEntityGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			_LeavingQueue.Enqueue(key);
			return true;
		}

		/// <inheritdoc />
		public bool Add([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return _ContainedEntities.Add(guid);
		}

		/// <inheritdoc />
		public bool Remove([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return _ContainedEntities.Remove(guid);
		}
	}
}
