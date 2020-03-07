using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using FreecraftCore.Serializer;
using GladNet;

namespace GladMMO
{
	[WireDataContract]
	public sealed class ServerPacketHeader : IPacketHeader, ISerializationEventListener
	{
		/// <inheritdoc />
		public int PacketSize { get; private set; }

		//We do NOT subtract 2 bytes for the opcode because the opcode
		//is apart of the payload deserialization Type information
		/// <inheritdoc />
		public int PayloadSize => PacketSize;

		[KnownSize(2)]
		[WireMember(1)]
		private byte[] DefaultBytes;

		[Optional(nameof(IsLargePacket))]
		[WireMember(2)]
		private byte OptionalThirdByte;

		public bool IsLargePacket => (DefaultBytes[0] & 0x80) != 0;

		/// <inheritdoc />
		public ServerPacketHeader(int packetSize)
		{
			if(packetSize <= 0) throw new ArgumentOutOfRangeException(nameof(packetSize));

			PacketSize = packetSize;
		}

		//Serializer ctor
		private ServerPacketHeader()
		{

		}

		/// <inheritdoc />
		public void OnBeforeSerialization()
		{
			byte[] bytes = IncomingClientPacketHeader.EncodePacketSize(PacketSize);

			DefaultBytes = bytes;

			//We assume everything just works here
			if(IsLargePacket)
				OptionalThirdByte = bytes[2];
		}

		/// <inheritdoc />
		public void OnAfterDeserialization()
		{
			if(IsLargePacket)
				PacketSize = (int)(((((uint)DefaultBytes[0]) & 0x7F) << 16) | (((uint)DefaultBytes[1]) << 8) | OptionalThirdByte);
			else
				PacketSize = (int)(((uint)DefaultBytes[0]) << 8 | DefaultBytes[1]);
		}

	}
}