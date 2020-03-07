using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

			int index = 0;
			//TODO: Right now we're only doing heads, so this works but will break later.
			index = InitializeRotationIndex(context, NetworkMovementTrackerTypeFlags.Head, EntityGameObjectDirectory.Type.CameraRoot, rotations, index, directory);
			index = InitializeRotationIndex(context, NetworkMovementTrackerTypeFlags.RightHand, EntityGameObjectDirectory.Type.RightHand, rotations, index, directory);
			index = InitializeRotationIndex(context, NetworkMovementTrackerTypeFlags.LeftHand, EntityGameObjectDirectory.Type.LeftHand, rotations, index, directory);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int InitializeRotationIndex(NetworkMovementTrackerTypeFlags context, NetworkMovementTrackerTypeFlags flagToCheck, EntityGameObjectDirectory.Type gameObjectType, Quaternion[] rotations, int index, EntityGameObjectDirectory directory)
		{
			if ((context & flagToCheck) != 0)
				rotations[index++] = ComputeRelativeRotation(directory, gameObjectType);

			return index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int InitializePositionIndex(NetworkMovementTrackerTypeFlags context, NetworkMovementTrackerTypeFlags flagToCheck, EntityGameObjectDirectory.Type gameObjectType, Vector3[] positions, int index, EntityGameObjectDirectory directory)
		{
			if((context & flagToCheck) != 0)
				positions[index++] = ComputeRelativePosition(directory, gameObjectType);

			return index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Quaternion ComputeRelativeRotation(EntityGameObjectDirectory directory, EntityGameObjectDirectory.Type gameObjectType)
		{
			return directory.GetGameObject(gameObjectType).transform.localRotation;
		}

		private void InitializePositions(NetworkMovementTrackerTypeFlags context, Vector3[] positions)
		{
			EntityGameObjectDirectory directory = GameObjectDirectoryMappable.RetrieveEntity(LocalPlayerDetails.LocalPlayerGuid);

			int index = 0;
			index = InitializePositionIndex(context, NetworkMovementTrackerTypeFlags.Head, EntityGameObjectDirectory.Type.CameraRoot, positions, index, directory);
			index = InitializePositionIndex(context, NetworkMovementTrackerTypeFlags.RightHand, EntityGameObjectDirectory.Type.RightHand, positions, index, directory);
			index = InitializePositionIndex(context, NetworkMovementTrackerTypeFlags.LeftHand, EntityGameObjectDirectory.Type.LeftHand, positions, index, directory);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector3 ComputeRelativePosition(EntityGameObjectDirectory directory, EntityGameObjectDirectory.Type gameObjectType)
		{
			return directory.GetGameObject(gameObjectType).transform.localPosition;
		}

		//Fun fun fun, this is: https://en.wikipedia.org/wiki/Hamming_weight
		internal static UInt32 CountChangedTrackerFlags(NetworkMovementTrackerTypeFlags flags)
		{
			UInt32 v = (UInt32)flags;
			v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
			v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
			UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
			return c;
		}
	}
}
