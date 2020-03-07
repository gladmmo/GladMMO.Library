using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.Ping)]
	public sealed class ServerPingPacketPayload : GameServerPacketPayload
	{
		//Send nothing, similar to WoW's ping and pong mechanisms.
		public ServerPingPacketPayload()
		{
			
		}
	}
}
