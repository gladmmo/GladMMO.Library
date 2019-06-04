using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Grants a character to the user of the type and name specified in the request.
	/// </summary>
	[JsonObject]
	public sealed class GladMMOPlayFabGrantCharacterToUserRequest
	{
		/// <summary>
		/// Non-unique display name of the character being granted (1-20 characters in length).
		/// </summary>
		[JsonProperty]
		public string CharacterName { get; private set; }

		/// <summary>
		/// Type of the character being granted; statistics can be sliced based on this value.
		/// </summary>
		[JsonProperty]
		public string CharacterType { get; private set; }

		/// <summary>
		/// Unique PlayFab assigned ID of the user on whom the operation will be performed.
		/// </summary>
		[JsonProperty]
		public string PlayFabId { get; private set; }

		public GladMMOPlayFabGrantCharacterToUserRequest(string characterName, string characterType, string playFabId)
		{
			if (string.IsNullOrWhiteSpace(characterName))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));
			if (string.IsNullOrWhiteSpace(characterType))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterType));
			if (string.IsNullOrWhiteSpace(playFabId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(playFabId));

			CharacterName = characterName;
			CharacterType = characterType;
			PlayFabId = playFabId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GladMMOPlayFabGrantCharacterToUserRequest()
		{
			
		}
	}
}
