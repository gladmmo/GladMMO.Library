using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	//TODO: Generalize serverside player interactions
	[JsonObject]
	public sealed class ZoneServerAvatarPedestalInteractionCharacterRequest
	{
		[JsonProperty]
		public ObjectGuid CharacterGuid { get; private set; }

		[JsonProperty]
		public int AvatarPedestalId { get; private set; }

		public ZoneServerAvatarPedestalInteractionCharacterRequest([NotNull] ObjectGuid characterGuid, int avatarPedestalId)
		{
			if (avatarPedestalId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarPedestalId));

			CharacterGuid = characterGuid ?? throw new ArgumentNullException(nameof(characterGuid));
			AvatarPedestalId = avatarPedestalId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		protected ZoneServerAvatarPedestalInteractionCharacterRequest()
		{

		}
	}
}
