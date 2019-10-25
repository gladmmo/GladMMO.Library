using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterFriendAddResponseModel
	{
		/// <summary>
		/// The GUID for the newly added player.
		/// </summary>
		[JsonProperty]
		public NetworkEntityGuid NewFriendEntityGuid { get; private set; }

		public CharacterFriendAddResponseModel([JetBrains.Annotations.NotNull] NetworkEntityGuid newFriendEntityGuid)
		{
			NewFriendEntityGuid = newFriendEntityGuid ?? throw new ArgumentNullException(nameof(newFriendEntityGuid));
		}

		[JsonConstructor]
		private CharacterFriendAddResponseModel()
		{
			
		}
	}
}
