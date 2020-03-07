using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ClickToMove)]
	public sealed class ClientSetClickToMovePathRequestPayload : GameClientPacketPayload
	{
		[ProtoMember(1)]
		public PathBasedMovementData PathData { get; private set; }

		public ClientSetClickToMovePathRequestPayload([NotNull] PathBasedMovementData pathData)
		{
			PathData = pathData ?? throw new ArgumentNullException(nameof(pathData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ClientSetClickToMovePathRequestPayload()
		{
			
		}
	}
}
