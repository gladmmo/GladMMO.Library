using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// The request that comes from a ZoneServer that MUST be validated
	/// with elevated or additional roles. Users must NOT be able to send this request.
	/// </summary>
	[JsonObject]
	public sealed class ZoneServerTryClaimSessionRequest
	{
		//WARNING: Do NOT trust that the client has provided valid account and character id pairs.
		//Players WILL try to cheat and spoof this. Check it on the zone server if possible, for validity
		//before sending the request to the game server. The gameserver should NOT trust it either.
		/// <summary>
		/// The player id the of the user.
		/// This is promised to be correct from the JWT.
		/// However that does not mean that the characterid 
		/// </summary>
		[JsonProperty]
		public int PlayerAccountId { get; private set; }
		
		/// <summary>
		/// The character id attempting to be claimed.
		/// This MAY not be actually linked to the provided <see cref="PlayerAccountId"/>.
		/// A player MAY spoof this to try to log in as other characters.
		/// </summary>
		[JsonProperty]
		public int CharacterId { get; private set; }

		/// <inheritdoc />
		public ZoneServerTryClaimSessionRequest(int playerAccountId, int characterId)
		{
			if(playerAccountId < 0) throw new ArgumentOutOfRangeException(nameof(playerAccountId));
			if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			PlayerAccountId = playerAccountId;
			CharacterId = characterId;
		}

		//Serializer ctor
		protected ZoneServerTryClaimSessionRequest()
		{
			
		}
	}
}
