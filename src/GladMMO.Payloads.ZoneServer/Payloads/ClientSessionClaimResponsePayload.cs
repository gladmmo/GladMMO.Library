using System; using FreecraftCore;
using System.ComponentModel;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Response payload sent in response to a <see cref="ClientSessionClaimRequestPayload"/>.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ClientSessionClaim)]
	public sealed class ClientSessionClaimResponsePayload : GameServerPacketPayload, IResponseModel<ClientSessionClaimResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[ProtoMember(1)]
		public ClientSessionClaimResponseCode ResultCode { get; }

		/// <inheritdoc />
		[ProtoIgnore]
		public bool isSuccessful => ResultCode == ClientSessionClaimResponseCode.Success;

		/// <inheritdoc />
		public ClientSessionClaimResponsePayload(ClientSessionClaimResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ClientSessionClaimResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(ClientSessionClaimResponseCode));

			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ClientSessionClaimResponsePayload()
		{
			
		}
	}
}
