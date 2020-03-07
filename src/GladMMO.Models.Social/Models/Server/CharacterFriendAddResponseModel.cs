using System; using FreecraftCore;
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
		public ObjectGuid NewFriendEntityGuid { get; private set; }

		public CharacterFriendAddResponseModel([JetBrains.Annotations.NotNull] ObjectGuid newFriendEntityGuid)
		{
			NewFriendEntityGuid = newFriendEntityGuid ?? throw new ArgumentNullException(nameof(newFriendEntityGuid));
		}

		[JsonConstructor]
		private CharacterFriendAddResponseModel()
		{
			
		}
	}
}
