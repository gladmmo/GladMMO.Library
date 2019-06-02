using System;
using GladNet;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Base type for the game packets that clients send.
	/// </summary>
	[ProtoContract(UseProtoMembersOnly = true)]
	public abstract class GameClientPacketPayload : IGamePacketPayload
	{
		//We don't need anything in the base packet actually.
		public GameClientPacketPayload()
		{

		}
	}
}