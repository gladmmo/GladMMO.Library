using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Payload for the client to request an interaction with a networked object.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.NetworkedObjectInteract)]
	public sealed class ClientInteractNetworkedObjectRequestPayload : GameClientPacketPayload
	{
		[ProtoMember(1)]
		public NetworkEntityGuid TargetObjectGuid { get; private set; }

		public ClientInteractNetworkedObjectRequestPayload([NotNull] NetworkEntityGuid targetObjectGuid)
		{
			TargetObjectGuid = targetObjectGuid ?? throw new ArgumentNullException(nameof(targetObjectGuid));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientInteractNetworkedObjectRequestPayload()
		{

		}
	}
}
