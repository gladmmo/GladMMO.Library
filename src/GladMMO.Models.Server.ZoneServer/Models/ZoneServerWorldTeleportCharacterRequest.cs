using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	//TODO: Generalize serverside player interactions
	[JsonObject]
	public sealed class ZoneServerWorldTeleportCharacterRequest
	{
		[JsonProperty]
		public ObjectGuid CharacterGuid { get; private set; }

		[JsonProperty]
		public int WorldTeleporterId { get; private set; }

		public ZoneServerWorldTeleportCharacterRequest([NotNull] ObjectGuid characterGuid, int worldTeleporterId)
		{
			if (worldTeleporterId <= 0) throw new ArgumentOutOfRangeException(nameof(worldTeleporterId));

			CharacterGuid = characterGuid ?? throw new ArgumentNullException(nameof(characterGuid));
			WorldTeleporterId = worldTeleporterId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		protected ZoneServerWorldTeleportCharacterRequest()
		{

		}
	}
}
