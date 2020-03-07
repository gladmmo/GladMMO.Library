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
		private readonly HashSet<ObjectGuid> _ContainedEntities = new HashSet<ObjectGuid>(NetworkGuidEqualityComparer<ObjectGuid>.Instance);

		/// <summary>
		/// Ordered queue of leaving entites.
		/// Should be contained in <see cref="ContainedEntities"/>
		/// </summary>
		private readonly ObjectGuidQueue _LeavingQueue = new ObjectGuidQueue();

		/// <summary>
		/// Ordered queue of entering entites.
		/// Not contained in <see cref="ContainedEntities"/>
		/// </summary>
		private readonly ObjectGuidQueue _EnteringQueue = new ObjectGuidQueue();

		/// <summary>
		/// Represents the contained entites.
		/// </summary>
		public IReadOnlyCollection<ObjectGuid> ContainedEntities => _ContainedEntities;

		/// <summary>
		/// The collection of enties that are queued for leaving the tile.
		/// They have not left the title if they are in this collection, so won't be in <see cref="ContainedEntities"/>.
		/// They will leave the title in the next update.
		/// </summary>
		public IReadOnlyCollection<ObjectGuid> QueuedLeavingEntities => _LeavingQueue;

		/// <summary>
		/// The collection of enties that are queued for entering the tile.
		/// They have not actually joined the tile so won't be in <see cref="ContainedEntities"/>.
		/// They will join the tile in the next update.
		/// </summary>
		public IReadOnlyCollection<ObjectGuid> QueuedJoiningEntities => _EnteringQueue;

		/// <inheritdoc />
		public IDequeable<ObjectGuid> LeavingDequeueable => _LeavingQueue;

		/// <inheritdoc />
		public IDequeable<ObjectGuid> EnteringDequeueable => _EnteringQueue;

		/// <inheritdoc />
		public void Register([NotNull] ObjectGuid key, [NotNull] ObjectGuid value)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));
			if(value == null) throw new ArgumentNullException(nameof(value));

			//Both key and value are the same
			_EnteringQueue.Add(value);
		}

		/// <inheritdoc />
		public bool Contains([NotNull] ObjectGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			return _ContainedEntities.Contains(key);
		}

		/// <inheritdoc />
		public ObjectGuid Retrieve(ObjectGuid key)
		{
			if(!_ContainedEntities.Contains(key))
				throw new InvalidOperationException($"Provided Key: {key} does not exist in the tile.");

			return key;
		}

		/// <inheritdoc />
		public bool Unregister([NotNull] ObjectGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			_LeavingQueue.Add(key);
			return true;
		}

		/// <inheritdoc />
		public bool Add([NotNull] ObjectGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return _ContainedEntities.Add(guid);
		}

		/// <inheritdoc />
		public bool Remove([NotNull] ObjectGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			return _ContainedEntities.Remove(guid);
		}
	}
}
