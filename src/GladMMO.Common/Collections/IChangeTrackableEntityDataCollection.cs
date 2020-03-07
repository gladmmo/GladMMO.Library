using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement change tracking
	/// entity data.
	/// </summary>
	public interface IChangeTrackableEntityDataCollection : IEntityDataFieldContainer
	{
		/// <summary>
		/// The collection that tracks the dirty changes in values.
		/// </summary>
		WireReadyBitArray ChangeTrackingArray { get; }

		/// <summary>
		/// Clears the tracked changes in the <see cref="ChangeTrackingArray"/>
		/// </summary>
		void ClearTrackedChanges();

		/// <summary>
		/// Indicates if the collection has pending changes
		/// being tracked.
		/// </summary>
		bool HasPendingChanges { get; }
	}
}
