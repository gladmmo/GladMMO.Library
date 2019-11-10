using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		public enum InteractType : byte
		{
			Unknown = 0,
			Interaction = 1,
			Selection = 2
		}

		/// <summary>
		/// The guid of the object to request an interaction with.
		/// </summary>
		[ProtoMember(1)]
		public NetworkEntityGuid TargetObjectGuid { get; private set; }

		/// <summary>
		/// The interaction type.
		/// </summary>
		[ProtoMember(2)]
		public InteractType InteractionType { get; private set; }

		public ClientInteractNetworkedObjectRequestPayload([NotNull] NetworkEntityGuid targetObjectGuid, InteractType interactionType)
		{
			if (!Enum.IsDefined(typeof(InteractType), interactionType)) throw new InvalidEnumArgumentException(nameof(interactionType), (int) interactionType, typeof(InteractType));

			TargetObjectGuid = targetObjectGuid ?? throw new ArgumentNullException(nameof(targetObjectGuid));
			InteractionType = interactionType;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientInteractNetworkedObjectRequestPayload()
		{

		}
	}
}
