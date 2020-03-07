using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Payload sent by the client when it wishes
	/// to change avatars in a world. The zone is partially responsibilit for
	/// validating that it's a valid avatar and that they can switch to it in the world.
	/// World creators may have a way to limit changing or valid avatars.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ModelChangeRequest)]
	public sealed class PlayerModelChangeRequestPayload : GameClientPacketPayload
	{
		/// <summary>
		/// The ModelId that the player wants
		/// to change to.
		/// </summary>
		[ProtoMember(1)]
		public int ModelId { get; private set; }

		/// <inheritdoc />
		public PlayerModelChangeRequestPayload(int modelId)
		{
			//0 is valid, it will refer to the default avatar.
			if(modelId < 0) throw new ArgumentOutOfRangeException(nameof(modelId));
			ModelId = modelId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private PlayerModelChangeRequestPayload()
		{

		}
	}
}