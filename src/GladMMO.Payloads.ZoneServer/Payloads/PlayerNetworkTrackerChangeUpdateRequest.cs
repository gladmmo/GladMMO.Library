using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace GladMMO
{
	//I decided not to go with delta compressed updates.
	//TODO: If/When we switch to UDP we should should serialize these differently
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.PlayerTrackerDataChange)]
	public sealed class PlayerNetworkTrackerChangeUpdateRequest : GameClientPacketPayload
	{
		[ProtoMember(1)]
		public NetworkMovementTrackerTypeFlags UpdateFields { get; private set; }

		[ProtoMember(2)]
		public Vector3[] TrackerPositionUpdates { get; private set; }

		[ProtoMember(3)]
		public Quaternion[] TrackerRotationUpdates { get; private set; }

		public PlayerNetworkTrackerChangeUpdateRequest(NetworkMovementTrackerTypeFlags updateFields, [NotNull] Vector3[] trackerPositionUpdates, [NotNull] Quaternion[] trackerRotationUpdates)
		{
			if (trackerPositionUpdates == null) throw new ArgumentNullException(nameof(trackerPositionUpdates));
			if (trackerRotationUpdates == null) throw new ArgumentNullException(nameof(trackerRotationUpdates));
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
}
