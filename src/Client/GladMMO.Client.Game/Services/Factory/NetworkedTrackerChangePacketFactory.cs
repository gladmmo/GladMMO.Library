using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class NetworkedTrackerChangePacketFactory : IFactoryCreatable<PlayerNetworkTrackerChangeUpdateRequest, NetworkMovementTrackerTypeFlags>
	{
		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		private ILocalPlayerDetails LocalPlayerDetails { get; }

		public NetworkedTrackerChangePacketFactory(
			[NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> gameObjectDirectoryMappable,
			[NotNull] ILocalPlayerDetails localPlayerDetails)
		{
			GameObjectDirectoryMappable = gameObjectDirectoryMappable ?? throw new ArgumentNullException(nameof(gameObjectDirectoryMappable));
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
		}

		public PlayerNetworkTrackerChangeUpdateRequest Create(NetworkMovementTrackerTypeFlags context)
		{
			uint count = CountChangedTrackerFlags(context);

			//TODO: We should pool these.
			Vector3[] positions = new Vector3[count];
			Quaternion[] rotations = new Quaternion[count];

			InitializePositions(context, positions);
			InitializeRotations(context, rotations);

			return new PlayerNetworkTrackerChangeUpdateRequest(context, positions, rotations);
		}

		private void InitializeRotations(NetworkMovementTrackerTypeFlags context, Quaternion[] rotations)
		{
			EntityGameObjectDirectory directory = GameObjectDirectoryMappable.RetrieveEntity(LocalPlayerDetails.LocalPlayerGuid);

			//TODO: Right now we're only doing heads, so this works but will break later.
			if ((context & NetworkMovementTrackerTypeFlags.Head) != 0)
			{
				//TODO: Optimize this.
				//Relative rotation from root gameobject rotation to camera rotation.
				//it's B - A where A is the root quaternion
				Quaternion relative = Quaternion.Inverse(directory.GetGameObject(EntityGameObjectDirectory.Type.Root).transform.rotation) * directory.GetGameObject(EntityGameObjectDirectory.Type.CameraRoot).transform.rotation;
				rotations[0] = relative;
			}
		}

		private void InitializePositions(NetworkMovementTrackerTypeFlags context, Vector3[] positions)
		{
			EntityGameObjectDirectory directory = GameObjectDirectoryMappable.RetrieveEntity(LocalPlayerDetails.LocalPlayerGuid);

			//TODO: Right now we're only doing heads, so this works but will break later.
			if ((context & NetworkMovementTrackerTypeFlags.Head) != 0)
			{
				Vector3 relative = directory.GetGameObject(EntityGameObjectDirectory.Type.CameraRoot).transform.position - directory.GetGameObject(EntityGameObjectDirectory.Type.Root).transform.position;
				positions[0] = relative;
			}
		}

		internal static UInt32 CountChangedTrackerFlags(NetworkMovementTrackerTypeFlags skills)
		{
			UInt32 v = (UInt32)skills;
			v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
			v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
			UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
			return c;
		}
	}
}
