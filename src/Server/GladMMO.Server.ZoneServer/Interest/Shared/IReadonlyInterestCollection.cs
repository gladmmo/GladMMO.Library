using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyInterestCollection
	{
		/// <summary>
		/// Represents the contained entites.
		/// </summary>
		IReadOnlyCollection<NetworkEntityGuid> ContainedEntities { get; }

		/// <summary>
		/// The collection of enties that are queued for leaving the tile.
		/// They have not left the title if they are in this collection, so won't be in <see cref="ContainedEntities"/>.
		/// They will leave the title in the next update.
		/// </summary>
		IReadOnlyCollection<NetworkEntityGuid> QueuedLeavingEntities { get; }

		/// <summary>
		/// The collection of enties that are queued for entering the tile.
		/// They have not actually joined the tile so won't be in <see cref="ContainedEntities"/>.
		/// They will join the tile in the next update.
		/// </summary>
		IReadOnlyCollection<NetworkEntityGuid> QueuedJoiningEntities { get; }
	}
}
