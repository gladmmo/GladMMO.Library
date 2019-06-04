using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public class GladMMOPlayFabGrantCharacterToUserResult
	{
		/// <summary>
		/// Unique identifier tagged to this character.
		/// </summary>
		[JsonProperty]
		public string CharacterId { get; private set; }

		public GladMMOPlayFabGrantCharacterToUserResult(string characterId)
		{
			if (string.IsNullOrWhiteSpace(characterId))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterId));

			CharacterId = characterId;
		}

		[JsonConstructor]
		private GladMMOPlayFabGrantCharacterToUserResult()
		{
			
		}
	}
}
