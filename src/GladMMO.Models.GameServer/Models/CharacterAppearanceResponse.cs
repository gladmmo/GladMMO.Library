using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterAppearanceResponse
	{
		//We may have more stuff eventually, right now we only have the model id.
		[JsonProperty]
		public int AvatarModelId { get; }

		public CharacterAppearanceResponse(int avatarModelId)
		{
			if (avatarModelId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarModelId));

			AvatarModelId = avatarModelId;
		}

		//Serializer ctor
		private CharacterAppearanceResponse()
		{

		}
	}
}
