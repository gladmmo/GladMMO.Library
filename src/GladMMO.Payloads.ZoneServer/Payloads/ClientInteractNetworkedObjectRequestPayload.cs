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
		public NetworkEntityGuid TargetGameObjectGuid { get; private set; }

		public ClientInteractNetworkedObjectRequestPayload([NotNull] NetworkEntityGuid targetGameObjectGuid)
		{
			TargetGameObjectGuid = targetGameObjectGuid ?? throw new ArgumentNullException(nameof(targetGameObjectGuid));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientInteractNetworkedObjectRequestPayload()
		{

		}
	}
}
