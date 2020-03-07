using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using ProtoBuf;

namespace GladMMO
{
	//This payload could probably be apart of the NetworkObjectVisibilityChangeEventPayload BUT it is much
	//simplier to seperate it into this one-time style packet.
	/// <summary>
	/// Event payload sent when the local player should be spawned, when its ready to spawn.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.PlayerSelfSpawn)]
	public sealed class PlayerSelfSpawnEventPayload : GameServerPacketPayload
	{
		//TODO: We can do what WoW does and send some Entity flags about it, like "stationary" or "living" and etc.
		/// <summary>
		/// The creation data for the local player.
		/// </summary>
		[ProtoMember(1)]
		public EntityCreationData CreationData { get; }

		/// <inheritdoc />
		public PlayerSelfSpawnEventPayload([NotNull] EntityCreationData creationData)
		{
			CreationData = creationData ?? throw new ArgumentNullException(nameof(creationData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected PlayerSelfSpawnEventPayload()
		{
			
		}
	}
}
