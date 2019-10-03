using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Payload for the client to request an interaction with a GameObject.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.GameObjectInteract)]
	public sealed class ClientInteractGameObjectRequestPayload : GameClientPacketPayload
	{
		[ProtoMember(1)]
		public NetworkEntityGuid TargetGameObjectGuid { get; private set; }

		public ClientInteractGameObjectRequestPayload([NotNull] NetworkEntityGuid targetGameObjectGuid)
		{
			TargetGameObjectGuid = targetGameObjectGuid ?? throw new ArgumentNullException(nameof(targetGameObjectGuid));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientInteractGameObjectRequestPayload()
		{

		}
	}
}
