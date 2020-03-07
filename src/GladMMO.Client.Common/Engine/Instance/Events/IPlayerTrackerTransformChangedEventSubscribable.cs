using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	//I decided not to go with delta compressed updates.
	//TODO: If/When we switch to UDP we should should serialize these differently
	[Obsolete("This is a stub from pre-TrinityCore times")]
	public sealed class PlayerNetworkTrackerChangeUpdateRequest
	{
		public NetworkMovementTrackerTypeFlags UpdateFields { get; private set; }

		public Vector3[] TrackerPositionUpdates { get; private set; }

		public Quaternion[] TrackerRotationUpdates { get; private set; }

		public PlayerNetworkTrackerChangeUpdateRequest(NetworkMovementTrackerTypeFlags updateFields, [NotNull] Vector3[] trackerPositionUpdates, [NotNull] Quaternion[] trackerRotationUpdates)
		{
			if(trackerPositionUpdates == null) throw new ArgumentNullException(nameof(trackerPositionUpdates));
			if(trackerRotationUpdates == null) throw new ArgumentNullException(nameof(trackerRotationUpdates));
			if(updateFields == NetworkMovementTrackerTypeFlags.None)
				throw new ArgumentException($"Cannot send {NetworkMovementTrackerTypeFlags.None} as no updates exist.");

			UpdateFields = updateFields;

			if(trackerPositionUpdates.Length != trackerRotationUpdates.Length)
				throw new ArgumentException($"Must send the same length of {nameof(trackerPositionUpdates)} and {nameof(trackerRotationUpdates)}");

			TrackerPositionUpdates = trackerPositionUpdates;
			TrackerRotationUpdates = trackerRotationUpdates;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private PlayerNetworkTrackerChangeUpdateRequest()
		{

		}
	}

	public interface IPlayerTrackerTransformChangedEventSubscribable
	{
		event EventHandler<PlayerTrackerTransformChangedEventArgs> OnTrackerTransformChanged;
	}

	public sealed class PlayerTrackerTransformChangedEventArgs : EventArgs
	{
		public EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> ChangeInformation { get; }

		public PlayerTrackerTransformChangedEventArgs([NotNull] EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> changeInformation)
		{
			ChangeInformation = changeInformation ?? throw new ArgumentNullException(nameof(changeInformation));
		}
	}
}
