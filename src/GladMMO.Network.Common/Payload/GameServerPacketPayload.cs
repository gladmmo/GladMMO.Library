using System;
using GladNet;
using ProtoBuf;

namespace GladMMO
{
	//TODO: PR to Protobuf-Net doc fix for EnumPassthru
	/// <summary>
	/// Base type for the game packets that the server send.
	/// </summary>
	[ProtoContract(UseProtoMembersOnly = true)]
	public abstract class GameServerPacketPayload : IGamePacketPayload
	{
		//We don't need anything in the base packet actually.
		public GameServerPacketPayload()
		{

		}
	}
}