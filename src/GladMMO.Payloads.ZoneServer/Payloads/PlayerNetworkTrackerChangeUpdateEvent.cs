using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace GladMMO
{
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.PlayerTrackerDataChange)]
	public sealed class PlayerNetworkTrackerChangeUpdateEvent : GameServerPacketPayload
	{
		[ProtoMember(1)]
		public EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> PlayerTrackerUpdate { get; }

		public PlayerNetworkTrackerChangeUpdateEvent([NotNull] EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> playerTrackerUpdate)
		{
			PlayerTrackerUpdate = playerTrackerUpdate ?? throw new ArgumentNullException(nameof(playerTrackerUpdate));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private PlayerNetworkTrackerChangeUpdateEvent()
		{
			
		}
	}
}
