using System;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Client payload sent by a client in an attempt to
	/// claim a an open session on the server.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.ClientSessionClaim)]
	public sealed class ClientSessionClaimRequestPayload : GameClientPacketPayload
	{
		//TODO: This is not save to sent over the wire in plaintext so it should be encrypted somehow.
		/// <summary>
		/// The JWT string that can be used to authorize
		/// the user.
		/// </summary>
		[ProtoMember(1, IsRequired = true)]
		public string JWT { get; }

		//WARNING: This could very well not match the accountid provided. People WILL attempt to exploit this. VALIDATE
		/// <summary>
		/// The character id to attempt to claim the session on.
		/// </summary>
		[ProtoMember(2)]
		public int CharacterId { get; }

		/// <inheritdoc />
		public ClientSessionClaimRequestPayload(string jwt, int characterId)
		{
			if(string.IsNullOrWhiteSpace(jwt)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(jwt));
			if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			JWT = jwt;
			CharacterId = characterId;
		}

		//Serializer ctor
		private ClientSessionClaimRequestPayload()
		{
			
		}
	}
}
