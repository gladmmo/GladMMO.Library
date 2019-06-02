using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	/*public sealed class LoggableUnknownOpcodePayload : GamePacketPayload
	{
		public int PacketLength { get; }

		public NetworkOperationCode OpCode { get; }

		public byte[] BinaryData { get; }

		/// <inheritdoc />
		public LoggableUnknownOpcodePayload(int packetLength, NetworkOperationCode opCode, [NotNull] byte[] binaryData)
		{
			if(packetLength <= 0) throw new ArgumentOutOfRangeException(nameof(packetLength));
			if(!Enum.IsDefined(typeof(NetworkOperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(NetworkOperationCode));

			PacketLength = packetLength;
			OpCode = opCode;
			BinaryData = binaryData ?? throw new ArgumentNullException(nameof(binaryData));
		}
	}*/
}
